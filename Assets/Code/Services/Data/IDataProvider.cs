using System;
using System.Collections.Generic;
using TMPro;

namespace SerjBal
{
    public interface IDataProvider : IService
    {
        Data Value { get; set; }
        public List<string> removableTextKeys  { get; set; }
        bool DataHasKey(IMenuItem menuItem, string key);
        void RenameKey(IMenuItem parent, string key, string newKey);
        void RemoveKey(IMenuItem menuItem);
        public List<ItemData> GetDataOf(IMenuItem menuItem);
        ItemData GetOrCreateDateData(string key = null, ItemData overrideData = null);
        ItemData GetOrCreateChannelData(string key, ItemData overrideData= null);
        ItemData GetOrCreateTimeData(string parentKey, string key, ItemData overrideData= null);
    }
}
