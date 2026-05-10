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
        public static SpecialModSettings<K, A, S> Instance { get; internal set; }

        protected Dictionary<K, S> settings = new Dictionary<K, S>();
        private Dictionary<S, object> settingsCache = new Dictionary<S, object>();

        protected SpecialModSettings()
        {
            SpecialModSettings<K>.Instance = this;
            Instance = this;

            foreach (K key in Enum.GetValues(typeof(K)))
            {
                A attr = SettingsUtility.GetSettingAttribute<K, A>(key);
                settings[key] = attr.MakeSetting<K, S>(SettingKeyPrefix, key);
            }
        }

        protected virtual string SettingKeyPrefix => "";

        public IEnumerable<S> All => settings.Values;

        public V Get<V>(object key)
        {
            foreach (Setting<K> setting in settings.Values)
            {
                if (setting.key.ToString().Equals(key.ToString()))
                {
                    return (V)setting.GetValue();
                }
            }
            throw new Exception("Setting not found for " + key);
        }

        public void Set<V>(object key, V value)
        {
            foreach (Setting<K> setting in settings.Values)
            {
                if (setting.key.ToString().Equals(key.ToString()))
                {
                    setting.SetValue(value);
                    return;
                }
            }
            throw new Exception("Setting not found for " + key);
        }

        protected virtual IEnumerable<S> AllSettings => settings.Values;

        public abstract void DrawModSettings(Rect rect);

        protected virtual bool SettingRequiresRestart(S setting) => setting.restartRequired;

        public virtual void Notify_ModSettingsOpened()
        {
            settingsCache.Clear();
            foreach (S setting in AllSettings.Where(s => SettingRequiresRestart(s)))
            {
                settingsCache[setting] = setting.GetValue();
            }
        }

        public virtual void Notify_ModSettingsClosed()
        {
            foreach (S setting in AllSettings.Where(s => SettingRequiresRestart(s)))
            {
                if (settingsCache.ContainsKey(setting) && !settingsCache[setting].Equals(setting.GetValue()))
                {
                    Find.WindowStack.Add(new Dialog_MessageBox("SpecialSauce_RestartRequiredMessage".Translate(), buttonAText: "SpecialSauce_RestartNow".Translate(), buttonAAction: GenCommandLine.Restart, buttonBText: "SpecialSauce_NotNow".Translate(), title: "SpecialSauce_RestartNow".Translate(), acceptAction: GenCommandLine.Restart));
                    break;
                }
            }
            settingsCache.Clear();
        }

        public override void ExposeData()
        {
            foreach (Setting<K> setting in settings.Values)
            {
                setting.ExposeData();
            }
        }
    }
}
