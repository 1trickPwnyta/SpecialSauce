using Verse;

namespace SpecialSauce.ModSettings
{
    public abstract class ModSettings_DLCPatch : ModSettings_Categorized
    {
        public class DLCSetting : Setting_Checkbox
        {
            public bool bugFix;

            public DLCSetting(string labelKey, string saveKey = null) : base(labelKey, saveKey) { }

            protected override string Label
            {
                get
                {
                    return (bugFix ? "SpecialSauce_BugFix".Translate() + ": " : TaggedString.Empty) + labelKey.Translate() + (restartRequired ? " " + "SpecialSauce_RestartRequired".Translate() : TaggedString.Empty);
                }
            }
        }
    }
}
