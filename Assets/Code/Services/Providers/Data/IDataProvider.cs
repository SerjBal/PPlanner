using System;

namespace SerjBal
{
    public interface IDataProvider : IService
    {
        void SetData(Data data);
        DateData GetDateData();
    }
}
