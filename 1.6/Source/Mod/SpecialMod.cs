using SpecialSauce.ModSettings;
using System;
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

        protected SpecialMod(ModContentPack content) : base(content)
        {
            mods[PackageId] = this;
        }

        protected abstract string PackageName { get; }

        protected abstract string PackageId { get; }

        public virtual void Initialize() { }
    }

    internal interface IModWithSettings
    {
        ISettings Settings { get; }
    }

    public abstract class SpecialMod<T> : SpecialMod, IModWithSettings where T : Verse.ModSettings, ISettings, new()
    {
        private ISettings settings;

        protected SpecialMod(ModContentPack content) : base(content)
        {
            if (LoadSettingsEarly)
            {
                settings = ModSettings;
            }
        }

        protected virtual bool LoadSettingsEarly => false;

        public ISettings Settings => settings;

        protected virtual ISettings ModSettings => GetSettings<T>();

        public override string SettingsCategory() => settings != null ? PackageName : "";

        public override void DoSettingsWindowContents(Rect inRect) => settings?.DrawModSettings(inRect);

        protected virtual void OnInitialized() { }

        public override sealed void Initialize()
        {
            if (!LoadSettingsEarly)
            {
                settings = ModSettings;
            }
            OnInitialized();
        }
    }
}
