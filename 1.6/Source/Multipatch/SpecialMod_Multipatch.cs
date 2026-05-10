using SpecialSauce.Mod;
using SpecialSauce.Xml;
using System;
using System.Collections.Generic;
using Verse;

namespace SpecialSauce.Multipatch
{
    public abstract class SpecialMod_Multipatch<T, K> : SpecialMod<T, K, MultipatchSettingAttribute, Setting_Multipatch<K>> where T : SpecialModSettings_Multipatch<K>, new() where K : Enum
    {
        protected SpecialMod_Multipatch(ModContentPack content) : base(content)
        {
        }

        protected override bool LoadSettingsEarly => true;

        protected override IEnumerable<IXmlAttributeHandler> XmlAttributeHandlers
        {
            get { yield return new XmlAttributeHandler_Compatibility_RequiresSettingEnabled<K>(Content); }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            bool hideRestartRequired = ModSettings.CompatibilityModeActive;
            foreach (Setting_Multipatch<K> setting in ModSettings.All)
            {
                setting.hideRestartRequired = hideRestartRequired;
            }
        }
    }
}
