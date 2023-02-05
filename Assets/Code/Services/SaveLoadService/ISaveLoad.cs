using System;

namespace SerjBal
{
    public interface ISaveLoad : IService
    {
        void Load(string date, Action onLoaded);
        void Save();
        void Initialize();
    }
}