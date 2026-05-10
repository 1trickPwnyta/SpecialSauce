using HarmonyLib;
using RimWorld;
using SpecialSauce.Mod;

namespace SpecialSauce.ModSettings
{
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
