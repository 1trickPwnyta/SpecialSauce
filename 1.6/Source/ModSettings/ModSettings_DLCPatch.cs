using HarmonyLib;
using SpecialSauce.Mod;
using System;
using UnityEngine;
using Verse;

namespace SpecialSauce.ModSettings
{
    public abstract class ModSettings_DLCPatch : ModSettings_Categorized
    {
        public class DLCSetting : Setting_Checkbox
        {
            public bool bugFix;

            public DLCSetting(string labelKey, string saveKey = null) : base(labelKey, saveKey) { }

            protected override bool DefaultValue => true;

            protected override string Label
            {
                get
                {
                    return (bugFix ? "SpecialSauce_BugFix".Translate() + ": " : TaggedString.Empty) + labelKey.Translate() + (restartRequired ? " " + "SpecialSauce_RestartRequired".Translate() : TaggedString.Empty);
                }
            }
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true)]
        public class HarmonyPatch_Compatibility : HarmonyAttribute
        {
            public HarmonyPatch_Compatibility(string modId, string settingKey)
            {
                ModSettings_DLCPatch settings = SpecialMod.Get(modId).Settings as ModSettings_DLCPatch;
                if ((bool)settings.compatibilityModeSetting.Value)
                {
                    if (!settings.Get<bool>(settingKey))
                    {
                        info.category = "DisabledByHarmonyPatch_Compatibility";
                    }
                }
            }
        }

        internal readonly Setting compatibilityModeSetting;

        public ModSettings_DLCPatch()
        {
            compatibilityModeSetting = new Setting_Checkbox
            (
                labelKey: "SpecialSauce_CompatibilityMode", 
                tipKey: "SpecialSauce_CompatibilityModeDesc", 
                saveKey: CompatibilityModeSaveKey, 
                paintable: false
            );
        }

        protected virtual string CompatibilityModeSaveKey => "CompatibilityMode";

        public override void DrawModSettings(Rect rect)
        {
            compatibilityModeSetting.DoInterface(ref rect);
            rect.yMin += 15f;
            base.DrawModSettings(rect);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            compatibilityModeSetting.ExposeData();
        }
    }
}
