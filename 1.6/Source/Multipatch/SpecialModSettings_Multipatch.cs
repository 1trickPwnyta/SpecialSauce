using SpecialSauce.ModSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpecialSauce.Multipatch
{
    public class SpecialModSettings_Multipatch<K> : SpecialModSettings_Categorized<K, Setting_Checkbox<K>>, ISettings_Compatibility where K : Enum
    {
        private readonly Setting_Checkbox<K> compatibilityModeSetting;

        public SpecialModSettings_Multipatch()
        {
            compatibilityModeSetting = new Setting_Checkbox<K>()
            {
                labelKey = "SpecialSauce_CompatibilityMode",
                tipKey = "SpecialSauce_CompatibilityModeDesc",
                saveKey = CompatibilityModeSaveKey,
                paintable = false,
                restartRequired = true
            };
        }

        protected override IEnumerable<Setting_Checkbox<K>> AllSettings => base.AllSettings.Prepend(compatibilityModeSetting);

        public bool CompatibilityModeActive => compatibilityModeSetting.value;

        protected virtual string CompatibilityModeSaveKey => "CompatibilityMode";

        public override void DrawModSettings(Rect rect)
        {
            compatibilityModeSetting.DoInterface(ref rect);
            rect.yMin += 15f;
            base.DrawModSettings(rect);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            compatibilityModeSetting.ExposeData();
        }
    }
}
