using System;
using System.Reflection;

namespace SpecialSauce.ModSettings
{
    public static class SettingsUtility
    {
        public static A GetSettingAttribute<K, A>(K key) where K : Enum where A : SettingAttribute => typeof(K).GetField(key.ToString()).GetCustomAttribute<A>();
    }
}
