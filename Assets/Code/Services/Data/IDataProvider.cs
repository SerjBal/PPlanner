using System;
using TMPro;

namespace SerjBal
{
    public interface IDataProvider : IService
    {
        Data Value { get; }
        void SetData(Data data);
        bool DataHasKey(IMenuItem menuItem, string key);

        void RemoveKey(IMenuItem menuItem, string key);
        ItemData GetOrCreateDateData(string key = null);
        ItemData GetOrCreateChannelData(string key);
        ItemData GetOrCreateTimeData(string parentKey, string key);
        
        
    }
}
