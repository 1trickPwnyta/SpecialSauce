using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace SpecialSauce.ModSettings
{
    public abstract class Setting<K> : IExposable where K : Enum
    {
        private const float INTERFACE_HEIGHT = 25f;

        public static T Get<T>(object key) => SpecialModSettings<K>.Instance.Get<T>(key);

        public static void Set<T>(object key, T value) => SpecialModSettings<K>.Instance.Set(key, value);

        public K key;
        public string labelKey;
        public string tipKey;
        public string saveKey;
        public Func<bool> visibilityGetter;
        public int indentLevel;
        public bool restartRequired;

        protected Setting() { }

        protected virtual string Label => labelKey.Translate();

        public abstract object GetValue();

        public abstract void SetValue(object value);

        protected abstract void DoInterfaceSub(Rect rect);

        public void DoInterface(ref Rect rect)
        {
            if (visibilityGetter == null || visibilityGetter())
            {
                Rect interfaceRect = rect;
                interfaceRect.height = INTERFACE_HEIGHT;
                DoInterfaceSub(interfaceRect);
                rect.yMin += interfaceRect.height;
            }
        }

        public void DoInterface(Listing_Standard listing)
        {
            if (visibilityGetter == null || visibilityGetter())
            {
                Rect interfaceRect = listing.GetRect(INTERFACE_HEIGHT);
                DoInterfaceSub(interfaceRect);
            }
        }

        public abstract void ExposeData();
    }

    public abstract class Setting<T, K> : Setting<K> where K : Enum
    {
        public T value;

        protected virtual T DefaultValue { get; }

        protected Setting()
        {
            value = DefaultValue;
        }

        public override object GetValue() => value;

        public override void SetValue(object value) => this.value = (T)value;
    }

    public class Setting_Checkbox<K> : Setting<bool, K> where K : Enum
    {
        public bool paintable = true;

        public Setting_Checkbox() { }

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
