using System;

namespace SpecialSauce.ModSettings
{
    public class CategorizedSettingAttribute : SettingAttribute
    {
        public readonly string categoryKey;

        public CategorizedSettingAttribute(string categoryKey, string labelKey = null, string tipKey = null, string saveKey = null, Type enablerType = null, int indentLevel = 0, bool restartRequired = false) : base(labelKey, tipKey, saveKey, enablerType, indentLevel, restartRequired)
        {
            this.categoryKey = categoryKey;
        }
    }
}
