using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        private IAppFactory _appFactory;

        public SaveLoad(ICoroutineRunner coroutineRunner, IDataProvider data, IProgress loaderScreen)
        {
            _coroutineRunner = coroutineRunner;
            _data = data;
            _loaderScreen = loaderScreen;
        }

        public void Initialize()
        {
            _appFactory = new Services().Single<IAppFactory>();
        }

        public void Load(string date, Action onLoaded)
        {
            string filePath = Path.Combine(Application.persistentDataPath, $"{date}.json");
            
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var data = JsonUtility.FromJson<ItemData>(json);
                Data newData = new Data
                {
                    DateItem = data
                };
                _data.SetData(newData);
                onLoaded?.Invoke();
            }
            else
            {
                var dateData = _data.GetOrCreateDateData(date);
                Data newData = new Data { DateItem = dateData};
                _data.SetData(newData);
                onLoaded?.Invoke();
            }
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(_data.Value.DateItem);
            string filePath = Path.Combine(Application.persistentDataPath, $"{_data.Value.DateItem.Key}.json");
            File.WriteAllText(filePath, json);
        }
        
        public void Save(IMenuItem menuItem, string key)
        {
            switch (menuItem.itemType)
            {
                case MenuItemType.Date:
                    _data.GetOrCreateChannelData(key);
                    break;
                case MenuItemType.Channel:
                    _data.GetOrCreateTimeData(menuItem.Key, key);
                    break;
                case MenuItemType.Time:
                    
                    break;
            }
            Save();
        }

        public void SaveText(string key)
        {
            
        }
        
        public void LoadFromServer(string date, Action onLoaded)
        {
            _coroutineRunner.StartCoroutine(LoadFromServerCourutine(date, onLoaded));
        }

        private void SaveToServer()
        {
            _coroutineRunner.StartCoroutine(SaveToServerCourutine());
        }

        private IEnumerator SaveToServerCourutine()
        {
            string json = JsonUtility.ToJson(_data.Value.DateItem);
            var path = $"";
            UnityWebRequest request = UnityWebRequest.Put(path, json);
            request.SendWebRequest();
           yield break;
        }
        private IEnumerator LoadFromServerCourutine(string date, Action onLoaded)
        {
            var path = $"";
            UnityWebRequest request = UnityWebRequest.Get( path);
                AsyncOperation operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                _loaderScreen.Progress = request.downloadProgress;
                yield return null;
            }
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("Error loading JSON: " + request.error);
                var dateData = _data.GetOrCreateDateData(date) as ItemData;
                Data newData = new Data { DateItem = dateData};
                _data.SetData(newData);
                onLoaded?.Invoke();
            }
            else
            {
                var data = JsonUtility.FromJson<ItemData>(request.downloadHandler.text);
                Data newData = new Data
                {
                    DateItem = data
                };
                _data.SetData(newData);
                onLoaded?.Invoke();
            }

            _loaderScreen.Progress = 1;
        }
    }
}