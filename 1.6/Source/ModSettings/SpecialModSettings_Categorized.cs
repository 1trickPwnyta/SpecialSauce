using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Verse;

namespace SpecialSauce.ModSettings
{
    public abstract class SpecialModSettings_Categorized<K, A, S> : SpecialModSettings<K, A, S> where K : Enum where A : CategorizedSettingAttribute where S : Setting<K>, new()
    {
        private Vector2 scrollPosition;
        private float y;

        private readonly Dictionary<string, Dictionary<K, S>> categorizedSettings = new Dictionary<string, Dictionary<K, S>>();

        protected SpecialModSettings_Categorized()
        {
            foreach (KeyValuePair<K, S> pair in settings)
            {
                CategorizedSettingAttribute attr = SettingsUtility.GetSettingAttribute<K, CategorizedSettingAttribute>(pair.Key);
                if (!categorizedSettings.ContainsKey(attr.categoryKey))
                {
                    categorizedSettings[attr.categoryKey] = new Dictionary<K, S>();
                }
                categorizedSettings[attr.categoryKey][pair.Key] = pair.Value;
            }
        }

        public override void DrawModSettings(Rect rect)
        {
            Rect viewRect = new Rect(0f, 0f, rect.width - 20f, y);
            Widgets.BeginScrollView(rect, ref scrollPosition, viewRect);
            Listing_Standard listing = new Listing_Standard() { maxOneColumn = true };
            listing.Begin(viewRect);

            string lastCategory = typeof(K).GetField(categorizedSettings.Values.Last().First().Key.ToString()).GetCustomAttribute<CategorizedSettingAttribute>().categoryKey;
            foreach (Dictionary<K, S> category in categorizedSettings.Values)
            {
                using (new TextBlock(GameFont.Medium))
                {
                    listing.Label(typeof(K).GetField(category.First().Key.ToString()).GetCustomAttribute<CategorizedSettingAttribute>().categoryKey.Translate());
                }
                listing.GapLine();
                foreach (Setting<K> setting in category.Values)
                {
                    setting.DoInterface(listing);
                }
                if (!category.Equals(lastCategory))
                {
                    listing.Gap();
                }
            }

            y = listing.CurHeight;
            listing.End();

            Widgets.EndScrollView();
        }
    }
}
