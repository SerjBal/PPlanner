using System;
using System.IO;
using UnityEngine;

namespace SerjBal
{
    public class SettingsProvider : ISettingsProvider
    {
        private readonly IDataProvider _data;
        private readonly string _postsColorSettingsDirectory;

        public SettingsProvider(IDataProvider dataProvider)
        {
            _data = dataProvider;
            _postsColorSettingsDirectory = Path.Combine(Const.DataPath, Const.SettingsDirectoryName, Const.PostsColorFileName);
        }


        public void SaveColorSettings(ColorSettingType settingType, Color color)
        {
            var name = $"{Enum.GetName(typeof(ColorSettingType), settingType)}.json";
            var path = Path.Combine(_postsColorSettingsDirectory, name);
            var saveData = new PostsColorSetting { value = color };
            _data.CreateFile(path, saveData);
        }

        public Color LoadColorSettings(ColorSettingType settingType)
        {
            var name = $"{Enum.GetName(typeof(ColorSettingType), settingType)}.json";
            var path = Path.Combine(_postsColorSettingsDirectory, name);
            if (File.Exists(path))
            {
                var loadData = _data.LoadFile<PostsColorSetting>(path);
                return loadData.value;
            }

            var indicatorsConfigs = Configurations.Instance.indicatorsConfig;
            switch (settingType)
            {
                case ColorSettingType.ContentUndone:
                    return indicatorsConfigs.contentPostUndoneColor;
                case ColorSettingType.ContentDone:
                    return indicatorsConfigs.contentPostDoneColor;
                case ColorSettingType.AdsUndone:
                    return indicatorsConfigs.adsPostUndoneColor;
                case ColorSettingType.AdsDone:
                    return indicatorsConfigs.adsPostDoneColor;
                case ColorSettingType.TimeProgress:
                    return indicatorsConfigs.timeProgressBarColor;
                default:
                    return Color.black;
            }
        }
    }
}