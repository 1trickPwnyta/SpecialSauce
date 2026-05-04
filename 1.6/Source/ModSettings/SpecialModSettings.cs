using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpecialSauce.ModSettings
{
    public abstract class SpecialModSettings : Verse.ModSettings
    {
        private static Dictionary<Type, SpecialModSettings> settings = new Dictionary<Type, SpecialModSettings>();

        public static SpecialModSettings Get<T>() where T : SpecialModSettings
        {
            if (settings.ContainsKey(typeof(T)))
            {
                return settings[typeof(T)];
            }
            return null;
        }

        protected SpecialModSettings()
        {
            settings[GetType()] = this;
        }

        public abstract T Get<T>(string labelKey);

        public abstract void Set<T>(string labelKey, T value);

        public abstract void DrawModSettings(Rect rect);
    }
}
