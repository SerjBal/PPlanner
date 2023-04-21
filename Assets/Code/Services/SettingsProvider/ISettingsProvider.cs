using UnityEngine;

namespace SerjBal
{
    public interface ISettingsProvider : IService
    {
        void SaveColorSettings(ColorSettingType settingType, Color color);
        Color LoadColorSettings(ColorSettingType settingType);
    }
}