using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SerjBal.Code.Sources;
using UnityEngine;
using UnityEngine.Networking;

namespace SerjBal
{
    public enum MenuItemType
    {
        Date, Channel, Time
    }
    

    public class DataProvider : IDataProvider
    {
        public Data Value { get; private set; }

        private readonly ITemplatesProvider _templates;
        public DataProvider(ITemplatesProvider templates) => _templates = templates;
        public void SetData(Data data) => Value = data;

        public bool DataHasKey(IMenuItem menuItem, string key)
        {
            return GetContentOf(menuItem).ContainsKey(key);
        }

        public void RemoveKey(IMenuItem menuItem, string key)
        {
            var content = GetContentOf(menuItem);
            if (content.ContainsKey(key)) content.RemoveKey(key);
        }

        private List<ItemData> GetContentOf(IMenuItem menuItem)
        {
            switch (menuItem.itemType)
            {
                case MenuItemType.Date:
                    return Value.DateItem.Content;
                    break;
                case MenuItemType.Channel:
                    return Value.DateItem.Content.Get(menuItem.Key).Content;
                    break;
                case MenuItemType.Time:
                    return Value.DateItem.Content.Get(menuItem.Parent.Key).Content.Get(menuItem.Key).Content;
                    break;
                default:
                    Debug.Log("No content found");
                    return null;
            }
        }

        public ItemData GetOrCreateDateData(string key = null)
        {
            if (key != null) Value.DateItem = new ItemData { Key = key, Content = new List<ItemData>() };
            return Value.DateItem;
        }

        public ItemData GetOrCreateChannelData(string key)
        {
            var date = GetOrCreateDateData();
            if (date.Content != null && date.Content.ContainsKey(key))
                return date.Content.Get(key);

            var newChannel = new ItemData { Key = key, Content = new List<ItemData>() };
            date.Content.Add(newChannel);
            return newChannel;
        }

        public ItemData GetOrCreateTimeData(string channelKey, string timeKey)
        {
            var channel = GetOrCreateChannelData(channelKey);
            if (channel.Content.ContainsKey(timeKey))
                return channel.Content.Get(timeKey);

            var textKey = PlayerPrefs.GetInt(Const.LastTextID, 0) + 1;
            var textKeyItem = new ItemData {Key = textKey.ToString() };
            var timeData = new ItemData { Key = timeKey, Content = new List<ItemData>()};
            timeData.Content.Add(textKeyItem);
            channel.Content.Add(timeData);
            PlayerPrefs.SetInt(Const.LastTextID, textKey);
            return timeData;
        }
    }
}