using System;
using TMPro;

namespace SerjBal
{
    public interface IDataProvider : IService
    {
        Data Data { get; }
        void SetData(Data data);
        IData GetDateData();
        bool DataHasKey(string key, string nameText);
    }
}
