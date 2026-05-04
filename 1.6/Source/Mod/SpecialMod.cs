using SpecialSauce.ModSettings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace SpecialSauce.Mod
{
    public abstract class SpecialMod : Verse.Mod
    {
        private static readonly Dictionary<string, SpecialMod> mods = new Dictionary<string, SpecialMod>();

        public static SpecialMod Get(string packageId)
        {
            if (mods.ContainsKey(packageId))
            {
                return mods[packageId];
            }
            return null;
        }

        public static IEnumerable<SpecialMod> All => mods.Values.ToList();

        private SpecialModSettings settings;

        protected SpecialMod(ModContentPack content) : base(content)
        {
            mods[PackageId] = this;
            if (LoadSettingsEarly)
            {
                settings = ModSettings as SpecialModSettings;
            }
        }

        protected abstract string PackageName { get; }

        protected abstract string PackageId { get; }

        protected virtual bool LoadSettingsEarly => false;

        protected abstract Verse.ModSettings ModSettings { get; }

        internal SpecialModSettings Settings => settings;

        protected virtual void OnInitialized() { }

        public override string SettingsCategory() => PackageName;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            Settings.DrawModSettings(inRect);
        }

        public void Initialize()
        {
            if (!LoadSettingsEarly)
            {
                settings = ModSettings as SpecialModSettings;
            }
            OnInitialized();
        }
    }

    public abstract class SpecialMod<T> : SpecialMod where T : SpecialModSettings, new()
    {
        protected SpecialMod(ModContentPack content) : base(content)
        {
        }

        protected override Verse.ModSettings ModSettings => GetSettings<T>();
    }
}
