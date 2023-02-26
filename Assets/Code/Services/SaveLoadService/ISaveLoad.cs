using System;

namespace SerjBal
{
    public interface ISaveLoad : IService
    {
        void Load(string keyDate, Action onLoaded);
        ItemData Load(string keyDate);
        void Save();
        void Save(string keyPath, ItemData data);
        void Save(string key, TextData data);
        TextData LoadText(string key);
        bool Exists(string key);
    }
}