using UnityEngine;

namespace SpecialSauce.ModSettings
{
    public interface IModSettings
    {
        T Get<T>(string labelKey);

        void Set<T>(string labelKey, T value);

        void DrawModSettings(Rect rect);
    }
}
