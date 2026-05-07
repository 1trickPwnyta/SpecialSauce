using SpecialSauce.Multipatch;
using System.Xml;
using Verse;

namespace SpecialSauce.Patching
{
    public class PatchOperationAdd_Compatibility : PatchOperationAdd
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "PatchOperation")]
        private XmlContainer modId;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "PatchOperation")]
        private XmlContainer settingKey;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            return MultipatchUtility.ApplyPatchOperation_Compatibility(modId, settingKey, () => base.ApplyWorker(xml));
        }
    }
}
