using SpecialSauce.ModSettings;
using System;
using Verse;

namespace SpecialSauce.Multipatch
{
    public class Setting_Multipatch<K> : Setting_Checkbox<K> where K : Enum
    {
        public bool hideRestartRequired;
        public bool bugFix;

        protected override bool DefaultValue => true;

        protected override string Label
        {
            get
            {
                return (bugFix ? "SpecialSauce_BugFix".Translate() + ": " : TaggedString.Empty) + labelKey.Translate() + (restartRequired && !hideRestartRequired ? " " + "SpecialSauce_RestartRequired".Translate() : TaggedString.Empty);
            }
        }
    }
}
