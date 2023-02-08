using System;
using TMPro;

namespace SerjBal
{
    public interface IDataProvider : IService
    {
        Data Value { get; }
        void SetData(Data data);
        IData GetDateData();
        bool DataHasKey(string key, string nameText);

        public TimeData GetOrCreateTimeData(string parentKey, string key);
        IData GetOrCreateChannelData(string key);
    }
}
