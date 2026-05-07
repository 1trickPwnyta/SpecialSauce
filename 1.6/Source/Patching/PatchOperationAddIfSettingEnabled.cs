using System.Xml;
using Verse;

namespace SpecialSauce.Patching
{
    public class PatchOperationAddIfSettingEnabled : PatchOperationAdd
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "PatchOperation")]
        private XmlContainer modId;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "PatchOperation")]
        private XmlContainer settingKey;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            return PatchOperationUtility.ApplyWorkerIfSettingEnabled(modId, settingKey, () => base.ApplyWorker(xml));
        }
    }
}
