using System;

namespace SerjBal
{
    public interface ISaveLoad : IService
    {
        void Load(string keyDate, Action onLoaded = null);
        void Save();
        void Save(string keyPath, ItemData overrideData);
        void SaveText(string key, TextData textData);
        TextData LoadText(string key);
    }
}