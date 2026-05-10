using System.Collections.Generic;
using Verse;

namespace SpecialSauce.Xml
{
    public static class XmlUtility
    {
        private static readonly Dictionary<ModContentPack, Dictionary<string, IXmlAttributeHandler>> registeredAttributeHandlers = new Dictionary<ModContentPack, Dictionary<string, IXmlAttributeHandler>>();

        public static void RegisterXmlAttributeHandler(IXmlAttributeHandler handler)
        {
            string attributeName = handler.AttributeName.ToLower();
            if (!registeredAttributeHandlers.ContainsKey(handler.Mod))
            {
                registeredAttributeHandlers[handler.Mod] = new Dictionary<string, IXmlAttributeHandler>();
            }
            if (registeredAttributeHandlers[handler.Mod].ContainsKey(attributeName))
            {
                if (registeredAttributeHandlers[handler.Mod][attributeName].GetType() != handler.GetType())
                {
                    Log.Error("XmlAttributeHandler " + registeredAttributeHandlers[handler.Mod][attributeName] + " already registered for XML attribute " + handler.AttributeName + ". " + handler + " will be ignored for this attribute.");
                }
            }
            else
            {
                registeredAttributeHandlers[handler.Mod][attributeName] = handler;
            }
        }

        public static IXmlAttributeHandler GetXmlAttributeHandler(ModContentPack mod, string attributeName) => registeredAttributeHandlers.TryGetValue(mod, out Dictionary<string, IXmlAttributeHandler> modHandlers) ? modHandlers.TryGetValue(attributeName.ToLower(), out IXmlAttributeHandler handler) ? handler : null : null;
    }
}
