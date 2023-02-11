using System;

namespace SerjBal
{
    public interface ISaveLoad : IService
    {
        void Load(string date, Action onLoaded = null);
        void Save();
        void Initialize();
        void Save(IMenuItem menuItem, string inputFieldText);
    }
}