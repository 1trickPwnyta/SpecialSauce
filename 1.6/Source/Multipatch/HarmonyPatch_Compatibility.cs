using HarmonyLib;
using SpecialSauce.Mod;
using System;

namespace SpecialSauce.Multipatch
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true)]
    public class HarmonyPatch_Compatibility : HarmonyAttribute
    {
        public const string EnabledCategory = "enabled";

        public HarmonyPatch_Compatibility(string modId, object settingKey)
        {
            info.category = EnabledCategory;
            ISettings_Compatibility settings = (SpecialMod.Get(modId) as IModWithSettings).Settings as ISettings_Compatibility;
            if (settings.CompatibilityModeActive)
            {
                if (!settings.Get<bool>(settingKey))
                {
                    info.category = "DisabledByHarmonyPatch_MultipatchCompatibility";
                }
            }
        }
    }
}
