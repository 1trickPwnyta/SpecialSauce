using UnityEngine;

namespace SpecialSauce.ModSettings
{
    public interface ISettings
    {
        T Get<T>(object key);

        void Set<T>(object key, T value);

        void DrawModSettings(Rect rect);

        void Notify_ModSettingsOpened();

        void Notify_ModSettingsClosed();
    }
}
