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

        public SaveLoad(ICoroutineRunner coroutineRunner, IDataProvider data, IProgress loaderScreen)
        {
            _coroutineRunner = coroutineRunner;
            _data = data;
            _loaderScreen = loaderScreen;
        }

        public void Load(string date, Action onLoaded = null)
        {
            _data.Value.DateItem = null;
            string filePath = Path.Combine(Const.DataPath, $"{date}.json");
            
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var data = JsonUtility.FromJson<ItemData>(json);
                Data newData = new Data
                {
                    DateItem = data
                };
                _data.Value = newData;
                onLoaded?.Invoke();
            }
            else
            {
                _data.GetOrCreateDateData(date);
                onLoaded?.Invoke();
            }

            _loaderScreen.Progress = 1;
            LoadFromServer(date);
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
                if (File.Exists(key)) File.Delete(key);
            }
            _data.removableTextKeys = new List<string>();
        }

        public void Save(IMenuItem menuItem, string key, ItemData overrideData = null)
        {
            switch (menuItem.itemType)
            {
                case MenuItemType.Date:
                    _data.GetOrCreateChannelData(key, overrideData);
                    break;
                case MenuItemType.Channel:
                    _data.GetOrCreateTimeData(menuItem.Key, key, overrideData);
                    break;
            }
            Save();
            SaveToServer();
        }

        public void SaveText(string key, TextData textData)
        {
            string json = JsonUtility.ToJson(textData);
            string filePath = Path.Combine(Const.DataPath, $"{key}.json");
            File.WriteAllText(filePath, json);
        }
        
        public TextData LoadText(string key)
        {
            string filePath = Path.Combine(Const.DataPath, $"{key}.json");
            
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonUtility.FromJson<TextData>(json);
            }
            else
            {
                return new TextData { text = "" };
            }
        }
        
        public void LoadFromServer(string date)
        {
            //_coroutineRunner.StartCoroutine(LoadFromServerCourutine(date));
        }

        private void SaveToServer()
        {
            //_coroutineRunner.StartCoroutine(SaveToServerCourutine());
        }

        private IEnumerator SaveToServerCourutine()
        {
            ItemData dateData = _data.Value.DateItem;
            string json = JsonUtility.ToJson(dateData);
            var path = Path.Combine(Const.ServerPath, dateData.Key);
            UnityWebRequest request = UnityWebRequest.Put(path, json);
            request.SendWebRequest();
           yield break;
        }
        private IEnumerator LoadFromServerCourutine(string date)
        {
            var path = Path.Combine(Const.ServerPath, date);
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