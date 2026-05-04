using Verse;

namespace SpecialSauce.Mod
{
    [StaticConstructorOnStartup]
    public static class ModInitializer
    {
        static ModInitializer()
        {
            foreach (SpecialMod mod in SpecialMod.All)
            {
                mod.Initialize();
            }
        }
    }
}
