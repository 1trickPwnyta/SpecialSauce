using SpecialSauce.Mod;
using SpecialSauce.Xml;
using System;
using Verse;

namespace SpecialSauce.Multipatch
{
    public class XmlAttributeHandler_Compatibility_RequiresSettingEnabled<K> : IXmlAttributeHandler where K : Enum
    {
        private readonly ModContentPack mod;

        public XmlAttributeHandler_Compatibility_RequiresSettingEnabled(ModContentPack mod)
        {
            this.mod = mod;
        }

        public ModContentPack Mod => mod;

        public string AttributeName => "RequiresSettingEnabled";

        public bool ShouldSkipProcessing(string value)
        {
            SpecialModSettings_Multipatch<K> settings = (SpecialMod.Get(mod.PackageId) as IModWithSettings).Settings as SpecialModSettings_Multipatch<K>;
            return settings.CompatibilityModeActive && !settings.Get<bool>(value);
        }
    }
}
