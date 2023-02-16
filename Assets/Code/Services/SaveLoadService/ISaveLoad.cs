using System;

namespace SerjBal
{
    public interface ISaveLoad : IService
    {
        void Load(string date, Action onLoaded = null);
        void Save();
        void Save(IMenuItem menuItem, string inputFieldText, ItemData overrideData);
        void SaveText(string key, TextData textData);
        TextData LoadText(string key);
    }
}