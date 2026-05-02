using Verse;

namespace SpecialSauce
{
    public class SpecialSauceMod : Mod
    {
        public const string PACKAGE_ID = "1trickPwnyta.specialsauce";
        public const string PACKAGE_NAME = "1trickPwnyta's Special Sauce";

        public SpecialSauceMod(ModContentPack content) : base(content)
        {
            Log.Info("Ready.");
        }
    }
}
