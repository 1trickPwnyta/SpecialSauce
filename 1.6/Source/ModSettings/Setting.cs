using System;
using Verse;

namespace SpecialSauce.ModSettings
{
    public abstract class Setting : IExposable
    {
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

    public abstract class Setting_Generic<T> : Setting
    {
        public T value;
        public T defaultValue;

        public override object Value
        {
            get { return value; }
            set { this.value = (T)value; }
        }

        protected Setting_Generic(string labelKey, string saveKey = null) : base(labelKey, saveKey) { }
    }

    public class Setting_Checkbox : Setting_Generic<bool>
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
            Scribe_Values.Look(ref value, saveKey, defaultValue);
        }
    }
}
