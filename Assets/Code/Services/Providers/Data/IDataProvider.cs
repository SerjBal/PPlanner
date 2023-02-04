using System;

namespace SerjBal
{
    public interface IDataProvider : IService
    {
        Data Data { get; }
        void SetData(Data data);
        IData GetDateData();
    }
}
