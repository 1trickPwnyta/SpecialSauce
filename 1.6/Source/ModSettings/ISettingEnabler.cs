using Verse;

namespace SpecialSauce.ModSettings
{
    public interface ISettingEnabler
    {
        bool Enabled();
    }

    public class SettingEnabler_Royalty : ISettingEnabler
    {
        public bool Enabled() => ModsConfig.RoyaltyActive;
    }

    public class SettingEnabler_Ideology : ISettingEnabler
    {
        public bool Enabled() => ModsConfig.IdeologyActive;
    }

    public class SettingEnabler_Biotech : ISettingEnabler
    {
        public bool Enabled() => ModsConfig.BiotechActive;
    }

    public class SettingEnabler_Anomaly : ISettingEnabler
    {
        public bool Enabled() => ModsConfig.AnomalyActive;
    }

    public class SettingEnabler_Odyssey : ISettingEnabler
    {
        public bool Enabled() => ModsConfig.OdysseyActive;
    }
}
