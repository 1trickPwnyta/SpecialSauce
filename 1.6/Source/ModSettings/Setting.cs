using RimWorld;
using System;
using Verse;

namespace SpecialSauce.ModSettings
{
    public abstract class Setting : IExposable
    {
        public static T Get<T, S>(string key) where S : SpecialModSettings => SpecialModSettings.Get<S>().Get<T>(key);

        public static void Set<T, S>(string key, T value) where S : SpecialModSettings => SpecialModSettings.Get<S>().Set(key, value);
        
        public string labelKey;
        public string saveKey;
        public Func<bool> visibilityGetter;
        public int indentLevel;
        public bool restartRequired;

        public Setting(string labelKey, string saveKey = null)
        {
            this.labelKey = labelKey;
            this.saveKey = saveKey ?? labelKey;
        }

        public abstract object Value { get; set; }

        protected virtual string Label => labelKey.Translate();

        public abstract void DoInterface(Listing_Standard listing);

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

        protected Setting(string labelKey, string saveKey = null) : base(labelKey, saveKey)
        {
            value = DefaultValue;
        }
    }

    public class Setting_Checkbox : Setting<bool>
    {
        public Setting_Checkbox(string labelKey, string saveKey = null) : base(labelKey, saveKey) { }

        public override void DoInterface(Listing_Standard listing)
        {
            if (visibilityGetter == null || visibilityGetter())
            {
                string indent = new string(' ', indentLevel * 2);
                listing.CheckboxLabeled(indent + Label, ref value);
            }
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref value, saveKey, DefaultValue);
        }
    }
}
