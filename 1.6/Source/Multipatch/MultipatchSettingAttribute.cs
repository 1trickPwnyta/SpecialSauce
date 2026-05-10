using SpecialSauce.ModSettings;
using System;

namespace SpecialSauce.Multipatch
{
    public class MultipatchSettingAttribute : CategorizedSettingAttribute
    {
        public readonly bool bugFix;

        public MultipatchSettingAttribute(string categoryKey, string labelKey = null, string tipKey = null, string saveKey = null, Type enablerType = null, int indentLevel = 0, bool restartRequired = false, bool bugFix = false) : base(categoryKey, labelKey, tipKey, saveKey, enablerType, indentLevel, restartRequired)
        {
            this.bugFix = bugFix;
        }

        public override S MakeSetting<K, S>(string prefix, K key)
        {
            Setting_Multipatch<K> setting = base.MakeSetting<K, S>(prefix, key) as Setting_Multipatch<K>;
            setting.bugFix = bugFix;
            return setting as S;
        }
    }
}
