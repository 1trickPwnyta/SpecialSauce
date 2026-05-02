using HarmonyLib;
using Verse;

namespace SpecialSauce
{
    public class SpecialSauceMod : Mod
    {
        public const string PACKAGE_ID = "specialsauce.1trickPwnyta";
        public const string PACKAGE_NAME = "Special Sauce";

        public SpecialSauceMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Info("Ready.");
        }
    }
}
