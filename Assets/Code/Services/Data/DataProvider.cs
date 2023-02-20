using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
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
        public bool HasKey(string keyPath)
        {
            var keyParts = keyPath.Split("/");
            switch (keyParts.Length)
            {
                case 1:
                    return Value.DateItem != null;
                case 2:
                    return Value.DateItem.Content.Get(keyParts[1]) != null;
                case 3:
                    return Value.DateItem.Content.Get(keyParts[1]).Content.Get(keyParts[2]) != null;
                default:
                    Debug.Log("Key path is corrupt!");
                    return false;
            }
        }

        public void RenameKey(string keyPath, string newKey)
        {
            ItemData data = GetOrCreateData(keyPath);
            data.Key = newKey;
        }

        public void RemoveKey(string keyPath)
        {
            void RemoveChilds(List<ItemData> content)
            {
                for (int i = 0; i < content.Count; i++)
                {
                    RemoveKey($"{keyPath}/{content[i].Key}");
                }
            }

            var keyParts = keyPath.Split("/");
            if (keyParts.Length == 3)
            {
                var timeData = Value.DateItem.Content.Get(keyParts[1]).Content.Get(keyParts[2]);
                var timeContent = timeData.Content;
                if (timeContent.Count > 0) removableTextKeys.Add(timeContent[0].Key);
            }
            else if (keyParts.Length == 2)
            {
                RemoveChilds(Value.DateItem.Content.Get(keyParts[1]).Content);
            }
            else if (keyParts.Length == 1)
            {
                RemoveChilds(Value.DateItem.Content);
            }
            SetData(keyPath, null);
        }

        public void SetData(string keyPath, ItemData data)
        {
            var keyParts = keyPath.Split("/");
            switch (keyParts.Length)
            {
                case 1:
                    if (data != null)
                    {
                        Value.DateItem = data;
                    }
                    break;
                case 2:
                    if (data == null)
                        Value.DateItem.Content.RemoveKey(keyParts[1]);
                    else
                        Value.DateItem.Content.Override(keyParts[1], data);
                    break;
                case 3:
                    if (data == null)
                        Value.DateItem.Content.Get(keyParts[1]).Content.RemoveKey(keyParts[2]);
                    else
                        Value.DateItem.Content.Get(keyParts[1]).Content.Override(keyParts[2], data);
                    break;
                default:
                    Debug.Log("Key path is corrupt!");
                    break;
            }
        }
        public ItemData GetOrCreateData(string keyPath)
        {
            var keyParts = keyPath.Split("/");
            switch (keyParts.Length)
            {
                case 1:
                    return GetOrCreateDateData(keyParts);
                case 2:
                    return GetOrCreateChannelData(keyParts);
                case 3:
                    return GetOrCreateTimeData(keyParts);
                default:
                    Debug.Log("Key path is corrupt!");
                    return null;
            }
        }
        
        public ItemData GetOrCreateDateData(string[] keyPath = null)
        {
            if (keyPath != null)
                if (Value.DateItem == null)
                {
                    var dateItem = new ItemData { Key = keyPath[0], Content = new List<ItemData>() };
                    Value = new Data { DateItem = dateItem };
                }

            return Value.DateItem;
        }

        public ItemData GetOrCreateChannelData(string[] keyParts)
        {
            ItemData channel;
            var keyChannel = keyParts[1];
            var dateContent = Value.DateItem.Content;
            if (dateContent.ContainsKey(keyChannel))
            {
                channel = dateContent.Get(keyChannel);
            }
            else
            {
                channel = new ItemData { Key = keyChannel, Content = new List<ItemData>() };
                dateContent.Add(channel);
            }
            return channel;
        }

        public ItemData GetOrCreateTimeData(string[] keyParts)
        {
            ItemData time;
            string keyChannel = keyParts[1];
            string keyTime = keyParts[2];
            var channelContent = Value.DateItem.Content.Get(keyChannel).Content;
            if (channelContent.ContainsKey(keyTime))
            {
                time = channelContent.Get(keyTime);
            }
            else
            {
                time = new ItemData { Key = keyTime, Content = new List<ItemData>()};
                channelContent.Add(time);
            }

            if (time.Content.Count==0)
            {
                var keyTextItem = new ItemData { Key = GenerateTextKey() };
                time.Content.Add(keyTextItem);
                
            }
            return time;
        }

        private string GenerateTextKey()
        {
            var textKey = PlayerPrefs.GetInt(Const.LastTextID, 0) + 1;
            PlayerPrefs.SetInt(Const.LastTextID, textKey);
            return textKey.ToString();
        }
    }
}