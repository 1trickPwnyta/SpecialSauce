using HarmonyLib;
using SpecialSauce.Mod;
using System;

namespace SpecialSauce.Harmony
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true)]
    public class HarmonyPatch_SettingEnabled : HarmonyAttribute
    {
        public HarmonyPatch_SettingEnabled(string modId, string settingKey)
        {
            if (!SpecialMod.Get(modId).Settings.Get<bool>(settingKey))
            {
                info.category = "DisabledByHarmonyPatchMod";
            }
        }
    }
}
