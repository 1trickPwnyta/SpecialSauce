using Verse;

namespace SpecialSauce
{
    public class Mod_SpecialSauce : Verse.Mod
    {
        public Mod_SpecialSauce(ModContentPack content) : base(content)
        {
            var harmony = new HarmonyLib.Harmony("1trickpwnyta.specialsauce");
            harmony.PatchAll();
            Log.Info("Ready.");
        }
    }
}
