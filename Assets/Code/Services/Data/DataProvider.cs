using System;
using System.Collections;
using System.IO;
using SerjBal.Code.Sources;
using UnityEngine;
using UnityEngine.Networking;

namespace SerjBal
{
    public class DataProvider : IDataProvider
    {
        public Data Data { get; private set; }

        private readonly ITemplatesProvider _templates;

        public DataProvider(ITemplatesProvider templates) => _templates = templates;
        public IData GetDateData() => Data.Date;
        public void SetData(Data data) => Data = data;

        public bool DataHasKey(string channelKey, string timeKey)
        {
            bool value = false;
            if (Data.Date.Content!=null && Data.Date.Content.ContainsKey(channelKey))
            {
                var channelContent = Data.Date.Content[channelKey].Content;
                if (channelContent!=null && channelContent.ContainsKey(timeKey)) value = true;
            }
            return value;
        }

        public TimeData GetTimeData(string parentKey, string key)
        {
            if (DataHasKey(parentKey, key))
            {
                return GetDateData().Content[parentKey].Content[key] as TimeData;
            }
            else
            {
                var textKey = PlayerPrefs.GetInt(Const.LastTextID, 0) + 1;
                PlayerPrefs.SetInt(Const.LastTextID, textKey);
                return new TimeData
                {
                    Key = key,
                    TextKey = textKey.ToString()
                };
            }
        }
    }
}