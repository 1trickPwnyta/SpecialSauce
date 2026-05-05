using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace SpecialSauce.ModSettings
{
    public abstract class Setting : IExposable
    {
        public static T Get<T, S>(string key) where S : SpecialModSettings => SpecialModSettings.Get<S>().Get<T>(key);

        public static void Set<T, S>(string key, T value) where S : SpecialModSettings => SpecialModSettings.Get<S>().Set(key, value);
        
        public string labelKey;
        public string tipKey;
        public string saveKey;
        public Func<bool> visibilityGetter;
        public int indentLevel;
        public bool restartRequired;

        public Setting(string labelKey, string tipKey = null, string saveKey = null)
        {
            this.labelKey = labelKey;
            this.tipKey = tipKey;
            this.saveKey = saveKey ?? labelKey;
        }

        public abstract object Value { get; set; }

        protected virtual string Label => labelKey.Translate();

        protected abstract void DoInterfaceSub(Rect rect);

        public void DoInterface(ref Rect rect)
        {
            if (visibilityGetter == null || visibilityGetter())
            {
                Rect interfaceRect = rect;
                interfaceRect.height = 30f;
                DoInterfaceSub(interfaceRect);
                rect.yMin += interfaceRect.height;
            }
        }

        public void DoInterface(Listing_Standard listing)
        {
            if (visibilityGetter == null || visibilityGetter())
            {
                Rect interfaceRect = listing.GetRect(30f);
                DoInterfaceSub(interfaceRect);
            }
        }

        public abstract void ExposeData();
    }

    public abstract class Setting<T> : Setting
    {
        public T value;

        protected virtual T DefaultValue { get; }
        
        public override object Value
        {
            get { return value; }
            set { this.value = (T)value; }
        }

        protected Setting(string labelKey, string tipKey = null, string saveKey = null) : base(labelKey, tipKey, saveKey)
        {
            value = DefaultValue;
        }
    }

    public class Setting_Checkbox : Setting<bool>
    {
        private bool paintable;

        public Setting_Checkbox(string labelKey, string tipKey = null, string saveKey = null, bool paintable = true) : base(labelKey, tipKey, saveKey)
        {
            this.paintable = paintable;
        }

        protected override void DoInterfaceSub(Rect rect)
        {
            string indent = new string(' ', indentLevel * 2);
            Widgets.CheckboxLabeled(rect, indent + Label, ref value, paintable: paintable);
            if (tipKey != null)
            {
                TooltipHandler.TipRegionByKey(rect, tipKey);
            }
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref value, saveKey, DefaultValue);
        }
    }
}
