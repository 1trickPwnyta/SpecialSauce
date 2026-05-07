using SpecialSauce.Mod;
using System;
using Verse;

namespace SpecialSauce.Patching
{
    public static class PatchOperationUtility
    {
        public static bool ApplyWorkerIfSettingEnabled(XmlContainer modId, XmlContainer settingKey, Func<bool> applyWorker)
        {
            string modIdText = modId.node.InnerText;
            string settingKeyText = settingKey.node.InnerText;
            return (SpecialMod.Get(modIdText) as IModWithSettings).Settings.Get<bool>(settingKeyText) == true ? applyWorker() : true;
        }
    }
}
