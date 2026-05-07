using SpecialSauce.ModSettings;

namespace SpecialSauce.Multipatch
{
    public interface ISettings_Compatibility : ISettings
    {
        bool CompatibilityModeActive { get; }
    }
}
