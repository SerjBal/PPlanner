using System;
using System.Collections.Generic;
using TMPro;

namespace SerjBal
{
    public interface IDataProvider : IService
    {
        Data Value { get; set; }
        public List<string> removableTextKeys  { get; set; }
        bool HasKey(string keyPath);
        void RenameKey(string keyPath, string newKey);
        void RemoveKey(string keyPath); 
        void SetData(string keyPath, ItemData data);
        ItemData GetOrCreateData(string keyPath);
        ItemData GetOrCreateDateData(string[] keyParts = null);
        ItemData GetOrCreateChannelData(string[] keyParts );
        ItemData GetOrCreateTimeData(string[] keyParts);
    }
}
