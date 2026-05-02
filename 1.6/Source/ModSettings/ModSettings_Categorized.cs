using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace SpecialSauce.ModSettings
{
    public abstract class ModSettings_Categorized : Verse.ModSettings, IModSettings
    {
        private Vector2 scrollPosition;
        private float y;

        public struct SettingsCategory
        {
            public string labelKey;
            public Setting[] settings;
        }

        protected abstract IEnumerable<SettingsCategory> Categories { get; }

        public void DrawModSettings(Rect rect)
        {
            Rect viewRect = new Rect(0f, 0f, rect.width - 20f, y);
            Widgets.BeginScrollView(rect, ref scrollPosition, viewRect);
            Listing_Standard listing = new Listing_Standard() { maxOneColumn = true };
            listing.Begin(viewRect);

            IEnumerable<SettingsCategory> categories = Categories;
            SettingsCategory lastCategory = Categories.Last();
            foreach (SettingsCategory category in categories)
            {
                using (new TextBlock(GameFont.Medium))
                {
                    listing.Label("$" + category.labelKey.Translate());
                }
                listing.GapLine();
                foreach (Setting setting in category.settings)
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

        public override void ExposeData()
        {
            foreach (SettingsCategory category in Categories)
            {
                foreach (Setting setting in category.settings)
                {
                    setting.ExposeData();
                }
            }
        }

        public T Get<T>(string labelKey)
        {
            foreach (SettingsCategory category in Categories)
            {
                foreach (Setting setting in category.settings)
                {
                    if (setting.labelKey == labelKey)
                    {
                        return (T)setting.Value;
                    }
                }
            }
            throw new Exception("Setting not found for " + labelKey);
        }

        public void Set<T>(string labelKey, T value)
        {
            foreach (SettingsCategory category in Categories)
            {
                foreach (Setting setting in category.settings)
                {
                    if (setting.labelKey == labelKey)
                    {
                        setting.Value = value;
                        return;
                    }
                }
            }
            throw new Exception("Setting not found for " + labelKey);
        }
    }
}
