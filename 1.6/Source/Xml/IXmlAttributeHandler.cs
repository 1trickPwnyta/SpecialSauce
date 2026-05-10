using Verse;

namespace SpecialSauce.Xml
{
    public interface IXmlAttributeHandler
    {
        ModContentPack Mod { get; }

        string AttributeName { get; }

        bool ShouldSkipProcessing(string value);
    }
}
