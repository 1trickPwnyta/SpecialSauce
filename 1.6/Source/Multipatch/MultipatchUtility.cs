using SpecialSauce.Mod;
using System;
using Verse;

namespace SpecialSauce.Multipatch
{
    public static class MultipatchUtility
    {
        public static bool ApplyPatchOperation_Compatibility(XmlContainer modId, XmlContainer settingKey, Func<bool> applyWorker)
        {
            string modIdText = modId.node.InnerText;
            string settingKeyText = settingKey.node.InnerText;
            ISettings_Compatibility settings = (SpecialMod.Get(modIdText) as IModWithSettings).Settings as ISettings_Compatibility;
            if (settings.CompatibilityModeActive)
            {
                return settings.Get<bool>(settingKeyText) == true ? applyWorker() : true;
            }
            else
            {
                return applyWorker();
            }
        }
    }
}
