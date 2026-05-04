using SpecialSauce.Mod;
using System.Xml;
using Verse;

namespace SpecialSauce.Patching
{
    public class PatchOperationReplaceIfSettingEnabled : PatchOperationReplace
    {
        private XmlContainer modId;
        private XmlContainer settingKey;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            string modIdText = modId.node.InnerText;
            string settingKeyText = settingKey.node.InnerText;
            return SpecialMod.Get(modIdText).Settings.Get<bool>(settingKeyText) == true ? base.ApplyWorker(xml) : true;
        }
    }
}
