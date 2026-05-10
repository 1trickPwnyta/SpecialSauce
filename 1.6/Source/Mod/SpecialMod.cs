using SpecialSauce.ModSettings;
using SpecialSauce.Xml;
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
            foreach (IXmlAttributeHandler handler in XmlAttributeHandlers)
            {
                XmlUtility.RegisterXmlAttributeHandler(handler);
            }
        }

        protected abstract string PackageName { get; }

        protected abstract string PackageId { get; }

        protected virtual IEnumerable<IXmlAttributeHandler> XmlAttributeHandlers => Enumerable.Empty<IXmlAttributeHandler>();

        public virtual void Initialize() { }
    }

    public interface IModWithSettings
    {
        ISettings Settings { get; }
    }

    public abstract class SpecialMod<T, K, A, S> : SpecialMod, IModWithSettings where T : SpecialModSettings<K, A, S>, ISettings, new() where K : Enum where A : SettingAttribute where S : Setting<K>, new()
    {
        private T settings;

        protected SpecialMod(ModContentPack content) : base(content)
        {
            if (LoadSettingsEarly)
            {
                settings = ModSettings;
            }
        }

        protected virtual bool LoadSettingsEarly => false;

        public ISettings Settings => settings;

        protected virtual T ModSettings => GetSettings<T>();

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
