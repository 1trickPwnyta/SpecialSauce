using System;
using Verse;

namespace SpecialSauce.ModSettings
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SettingAttribute : Attribute
    {
        public readonly string labelKey;
        public readonly string tipKey;
        public readonly string saveKey;
        public readonly ISettingEnabler enabler;
        public readonly int indentLevel;
        public readonly bool restartRequired;

        public SettingAttribute(string labelKey = null, string tipKey = null, string saveKey = null, Type enablerType = null, int indentLevel = 0, bool restartRequired = false)
        {
            this.labelKey = labelKey;
            this.tipKey = tipKey;
            this.saveKey = saveKey;
            if (enablerType != null)
            {
                enabler = Activator.CreateInstance(enablerType) as ISettingEnabler;
            }
            this.indentLevel = indentLevel;
            this.restartRequired = restartRequired;
        }

        public virtual S MakeSetting<K, S>(string prefix, K key) where K : Enum where S : Setting<K>, new()
        {
            S setting = new S();
            setting.key = key;
            setting.labelKey = labelKey ?? prefix + (!prefix.NullOrEmpty() ? "_" : "") + key.ToString();
            setting.tipKey = tipKey;
            setting.saveKey = saveKey ?? setting.labelKey;
            if (enabler != null)
            {
                setting.visibilityGetter = enabler.Enabled;
            }
            setting.indentLevel = indentLevel;
            setting.restartRequired = restartRequired;
            return setting;
        }
    }
}
