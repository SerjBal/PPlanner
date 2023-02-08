using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SerjBal.Code.Sources;
using UnityEngine;
using UnityEngine.Networking;

namespace SerjBal
{
    public class DataProvider : IDataProvider
    {
        public Data Value { get; private set; }

        private readonly ITemplatesProvider _templates;
        public DataProvider(ITemplatesProvider templates) => _templates = templates;
        public void SetData(Data data) => Value = data;

        public bool DataHasKey(string channelKey, string timeKey)
        {
            bool value = false;
            if (Value.Date.Content!=null && Value.Date.Content.ContainsKey(channelKey))
            {
                var channelContent = Value.Date.Content[channelKey].Content;
                if (channelContent!=null && channelContent.ContainsKey(timeKey)) value = true;
            }
            return value;
        }

        public IData GetDateData()
        {
            return Value.Date;
        }

        public IData GetOrCreateChannelData(string key)
        {
            IData channelData = null;
            var date = GetDateData();
            if (date.Content!=null && date.Content.ContainsKey(key)) channelData = date.Content[key];
            return channelData;
        }

        public TimeData GetOrCreateTimeData(string channelKey, string timeKey)
        {
            var channel = GetOrCreateChannelData(channelKey);
            if (channel.Content.ContainsKey(timeKey))
            {
                return channel.Content[timeKey] as TimeData;
            }
            else
            {
                var textKey = PlayerPrefs.GetInt(Const.LastTextID, 0) + 1;
                PlayerPrefs.SetInt(Const.LastTextID, textKey);
                return new TimeData { Key = timeKey, TextKey = textKey.ToString() };
            }
        }
    }
}