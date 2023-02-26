using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SerjBal.Code.Sources;
using UnityEngine;
using UnityEngine.Networking;

namespace SerjBal
{
    internal class SaveLoad : ISaveLoad
    {
        private readonly IDataProvider _data;
        private readonly IProgress _loaderScreen;
        private ICoroutineRunner _coroutineRunner;

        public SaveLoad(ICoroutineRunner coroutineRunner, IDataProvider data, IProgress loaderScreen)
        {
            _coroutineRunner = coroutineRunner;
            _data = data;
            _loaderScreen = loaderScreen;
        }

        public void Load(string keyDate, Action onLoaded)
        {
            _data.Value.DateItem = null;
            string filePath = Path.Combine(Const.DataPath, $"{keyDate}.json");
            
            if (Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var data = JsonUtility.FromJson<ItemData>(json);
                Data newData = new Data { DateItem = data };
                _data.Value = newData;
                onLoaded?.Invoke();
            }
            else
            {
                _data.GetOrCreateData(keyDate);
                onLoaded?.Invoke();
            }

            _loaderScreen.Progress = 1;
            LoadFromServer(keyDate);
        }
        
        public ItemData Load(string keyDate)
        {
            string filePath = Path.Combine(Const.DataPath, $"{keyDate}.json");
            
            if (Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var data = JsonUtility.FromJson<ItemData>(json);
                return data;
            }

            return null;
        }

        public void UpdateMenu() => new Services().Single<IGUIModelView>().UpdateMenu();

        public void Save()
        {
            string json = JsonUtility.ToJson(_data.Value.DateItem);
            string filePath = Path.Combine(Const.DataPath, $"{_data.Value.DateItem.Key}.json");
            File.WriteAllText(filePath, json);
            
            //remove text data if is
            foreach (var key in _data.removableTextKeys)
            {
                if (Exists(key)) File.Delete(key);
            }
            _data.removableTextKeys = new List<string>();
        }

        public void Save(string keyPath, ItemData data = null)
        {
            if (data == null)
                _data.GetOrCreateData(keyPath);
            else
                _data.SetData(keyPath, data);
            
            Save();
            SaveToServer();
        }

        public void Save(string key, TextData data)
        {
            string json = JsonUtility.ToJson(data);
            string filePath = Path.Combine(Const.DataPath, $"{key}.json");
            File.WriteAllText(filePath, json);
        }
        
        public TextData LoadText(string key)
        {
            string filePath = Path.Combine(Const.DataPath, $"{key}.json");
            
            if (Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonUtility.FromJson<TextData>(json);
            }
            else
            {
                return new TextData { text = "" };
            }
        }
        
        public bool Exists(string key) => File.Exists(key);
        
        private void LoadFromServer(string date)
        {
            //LoadFromServerAsync(date);
        }

        private void SaveToServer()
        {
           // SaveToServerAsync();
        }

        private async Task SaveToServerAsync()
        {
            ItemData dateData = _data.Value.DateItem;
            string json = JsonUtility.ToJson(dateData);
            var path = Path.Combine(Const.ServerPath, dateData.Key);
            using (UnityWebRequest request = UnityWebRequest.Put(path, json))
            {
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Save To Server Error: {request.error}");
                }
            }
        }
        private async Task LoadFromServerAsync(string date)
        {
            var path = Path.Combine(Const.ServerPath, date);
            using UnityWebRequest request = UnityWebRequest.Get(path);
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error loading JSON: " + request.error);
            }
            else
            {
                var data = JsonUtility.FromJson<ItemData>(request.downloadHandler.text);
                Data newData = new Data { DateItem = data };
                _data.Value = newData;
                UpdateMenu();
            }

            _loaderScreen.Progress = 1;
        }
    }
}