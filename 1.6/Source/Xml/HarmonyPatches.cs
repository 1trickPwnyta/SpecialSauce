using HarmonyLib;
using SpecialSauce.Harmony;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml;
using Verse;

namespace SpecialSauce.Xml
{
    [HarmonyPatch(typeof(LoadedModManager))]
    [HarmonyPatch(nameof(LoadedModManager.ParseAndProcessXML))]
    public static class Patch_LoadedModManager
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) => instructions.Transpile(list =>
        {
            Exception exception = null;
            try
            {
                int index = list.FindIndex(i => i.Calls(typeof(ModLister).Method(nameof(ModLister.AnyModActiveNoSuffix), new[] { typeof(IEnumerable<string>) })));
                if (index >= 0 && list.Count > index + 2 && list[index + 1].opcode == OpCodes.Brfalse && list[index + 2].opcode == OpCodes.Ldarg_1)
                {
                    object continueTarget = list[index + 1].operand;
                    List<Label> postContinueLabels = list[index + 2].labels;
                    list.InsertRange(index + 2, new[]
                    {
                        new CodeInstruction(OpCodes.Ldloc_S, 11) { labels = postContinueLabels.ListFullCopy() },
                        new CodeInstruction(OpCodes.Ldarg_1),
                        new CodeInstruction(OpCodes.Call, typeof(Patch_LoadedModManager).Method(nameof(ShouldSkipXmlNodeProcessing))),
                        new CodeInstruction(OpCodes.Brtrue, continueTarget)
                    });
                    postContinueLabels.Clear();
                    return;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }
            Log.Error("Failed to patch LoadedModManager.ParseAndProcessXML for custom XML attribute handling during modded Def loading." + (exception != null ? " An unexpected exception occurred: " + exception : ""));
        });

        private static bool ShouldSkipXmlNodeProcessing(XmlNode node, Dictionary<XmlNode, LoadableXmlAsset> assetLookup)
        {
            ModContentPack mod = assetLookup.TryGetValue(node)?.mod;
            if (mod != null && node.Attributes != null)
            {
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    IXmlAttributeHandler handler = XmlUtility.GetXmlAttributeHandler(mod, attribute.Name);
                    if (handler != null && handler.ShouldSkipProcessing(attribute.Value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
