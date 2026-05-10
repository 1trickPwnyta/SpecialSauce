using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace SpecialSauce.Multipatch
{
    public class SpecialModSettings_Multipatch<K> : SpecialModSettings_Categorized<K, MultipatchSettingAttribute, Setting_Multipatch<K>>, ISettings_Compatibility where K : Enum
    {
        private readonly Setting_Multipatch<K> compatibilityModeSetting;

        public SpecialModSettings_Multipatch()
        {
            compatibilityModeSetting = new Setting_Multipatch<K>()
            {
                value = false,
                labelKey = "SpecialSauce_CompatibilityMode",
                tipKey = "SpecialSauce_CompatibilityModeDesc",
                saveKey = CompatibilityModeSaveKey,
                paintable = false,
                restartRequired = true,
                placeCheckboxNearText = true
            };
        }

        protected override IEnumerable<Setting_Multipatch<K>> AllSettings => base.AllSettings.Prepend(compatibilityModeSetting);

        public bool CompatibilityModeActive => compatibilityModeSetting.value;

        protected virtual string CompatibilityModeSaveKey => "CompatibilityMode";

        public override void DrawModSettings(Rect rect)
        {
            Rect compatibilityRect = rect;
            compatibilityModeSetting.DoInterface(ref compatibilityRect);
            Rect controlsRect = rect;
            controlsRect.height = 35f;
            controlsRect.xMin = controlsRect.xMax - 120f;
            if (UIUtility.ButtonImageText(controlsRect.ContractedBy(3f), Widgets.CheckboxOffTex, "SpecialSauce_DisableAll".Translate()))
            {
                foreach (Setting_Multipatch<K> setting in base.AllSettings)
                {
                    setting.SetValue(false);
                }
                SoundDefOf.Tick_Low.PlayOneShotOnCamera();
            }
            controlsRect.x -= controlsRect.width;
            if (UIUtility.ButtonImageText(controlsRect.ContractedBy(3f), Widgets.CheckboxOnTex, "SpecialSauce_EnableAll".Translate()))
            {
                foreach (Setting_Multipatch<K> setting in base.AllSettings)
                {
                    setting.SetValue(true);
                }
                SoundDefOf.Tick_High.PlayOneShotOnCamera();
            }
            rect.yMin += 50f;
            base.DrawModSettings(rect);
        }

        public bool ShouldEnableCodeForSetting(K key) => !CompatibilityModeActive || Get<bool>(key);

        protected override bool SettingRequiresRestart(Setting_Multipatch<K> setting) => CompatibilityModeActive || base.SettingRequiresRestart(setting);

        public override void ExposeData()
        {
            base.ExposeData();
            compatibilityModeSetting.ExposeData();
        }
    }
}
