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
        public Data Value { get; set; }
        public List<string> removableTextKeys  { get; set; }

        private readonly ITemplatesProvider _templates;
        public DataProvider(ITemplatesProvider templates)
        {
            Value = new Data { DateItem = null};
            removableTextKeys = new List<string>();
            _templates = templates;
        }
        public bool DataHasKey(IMenuItem menuItem, string key) => GetDataOf(menuItem).ContainsKey(key);
        public void RenameKey(IMenuItem menuItem, string key, string newKey)
        {
            var content = GetDataOf(menuItem.Parent);
            if (content.ContainsKey(key)) content.Get(key).Key = newKey;
        }

        public void RemoveKey(IMenuItem menuItem)
        {
            if (menuItem.itemType == MenuItemType.Time)
            {
                if (menuItem.Childs.Count>0)
                {
                    removableTextKeys.Add(menuItem.Childs[0].Key);
                } 
            }
            else
            {
                for (int i = 0; i < menuItem.Childs.Count; i++)
                {
                    RemoveKey(menuItem.Childs[i]);
                }
            }

            if (menuItem.Parent==null)
            {
                GetDataOf(menuItem).Clear();
            }
            else
            {
                var content = GetDataOf(menuItem.Parent);
                content.RemoveKey(menuItem.Key);
            }
        }

        public List<ItemData> GetDataOf(IMenuItem menuItem)
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

        public ItemData GetOrCreateDateData(string key = null, ItemData overrideData = null)
        {
            if (key != null)
                if (Value.DateItem == null)
                {
                    var dateItem = new ItemData { Key = key, Content = new List<ItemData>() };
                    Value = new Data { DateItem = dateItem };
                }

            if (overrideData!=null) Value.DateItem = overrideData;
            return Value.DateItem;
        }

        public ItemData GetOrCreateChannelData(string key, ItemData overrideData = null)
        {
            var date = GetOrCreateDateData();
            if (date.Content != null && date.Content.ContainsKey(key))
            {
                if (overrideData != null) date.Content.Override(key, overrideData);
                return date.Content.Get(key);
            }
            

            var newChannel = new ItemData { Key = key, Content = new List<ItemData>() };
            date.Content.Add(newChannel);
            return newChannel;
        }

        public ItemData GetOrCreateTimeData(string channelKey, string timeKey, ItemData overrideData = null)
        {
            var channel = GetOrCreateChannelData(channelKey);
            if (channel.Content.ContainsKey(timeKey))
            {
                if (overrideData != null) channel.Content.Override(timeKey, overrideData);
                return channel.Content.Get(timeKey);
            }
            
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