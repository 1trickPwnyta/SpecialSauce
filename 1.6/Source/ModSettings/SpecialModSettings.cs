using HarmonyLib;
using RimWorld;
using SpecialSauce.Mod;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace SpecialSauce.ModSettings
{
    public static class SpecialModSettings<K> where K : Enum
    {
        public static ISettings Instance { get; internal set; }
    }

    public abstract class SpecialModSettings<K, A, S> : Verse.ModSettings, ISettings where K : Enum where A : SettingAttribute where S : Setting<K>, new()
    {
        protected Dictionary<K, S> settings = new Dictionary<K, S>();
        private Dictionary<S, object> settingsCache = new Dictionary<S, object>();

        protected SpecialModSettings()
        {
            SpecialModSettings<K>.Instance = this;

            foreach (K key in Enum.GetValues(typeof(K)))
            {
                A attr = SettingsUtility.GetSettingAttribute<K, A>(key);
                settings[key] = attr.MakeSetting<K, S>(Mod.Content.PackageId, key);
            }
        }

        public abstract V Get<V>(object key);

        public abstract void Set<V>(object key, V value);

        protected virtual IEnumerable<S> AllSettings => settings.Values;

        public abstract void DrawModSettings(Rect rect);

        public virtual void Notify_ModSettingsOpened()
        {
            settingsCache.Clear();
            foreach (S setting in AllSettings.Where(s => s.restartRequired))
            {
                settingsCache[setting] = setting.GetValue();
            }
        }

        public virtual void Notify_ModSettingsClosed()
        {
            foreach (S setting in AllSettings.Where(s => s.restartRequired))
            {
                if (settingsCache.ContainsKey(setting) && !settingsCache[setting].Equals(setting.GetValue()))
                {
                    Find.WindowStack.Add(new Dialog_MessageBox("SpecialSauce_RestartRequiredMessage".Translate(), buttonAText: "SpecialSauce_RestartNow".Translate(), buttonAAction: GenCommandLine.Restart, buttonBText: "SpecialSauce_NotNow".Translate(), title: "SpecialSauce_RestartNow".Translate(), acceptAction: GenCommandLine.Restart));
                    break;
                }
            }
            settingsCache.Clear();
        }
    }

    [HarmonyPatch(typeof(Dialog_ModSettings))]
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyPatch(new[] { typeof(Verse.Mod) })]
    public static class Patch_Dialog_ModSettings_Constructor
    {
        public static void Postfix(Verse.Mod mod) => (SpecialMod.Get(mod.Content.PackageId) as IModWithSettings)?.Settings?.Notify_ModSettingsOpened();
    }

    [HarmonyPatch(typeof(Dialog_ModSettings))]
    [HarmonyPatch(nameof(Dialog_ModSettings.PreClose))]
    public static class Patch_Dialog_ModSettings_PreClose
    {
        public static void Postfix(Verse.Mod ___mod) => (SpecialMod.Get(___mod.Content.PackageId) as IModWithSettings)?.Settings?.Notify_ModSettingsClosed();
    }
}
