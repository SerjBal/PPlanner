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
            _coroutineRunner.StartCoroutine(LoadLocal(date, onLoaded));
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(_data);
            File.WriteAllText(Const.DataPath, json);
            Debug.Log("LocalSave");
            SaveNetwork();
        }
        
        public void LoadNetwork()
        {
        }

        private void SaveNetwork()
        {
        }

        private IEnumerator LoadLocal(string date, Action onLoaded)
        {
            UnityWebRequest request = UnityWebRequest.Get( $"file://{Const.DataPath}/{date}");
            AsyncOperation operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                _loaderScreen.Progress = request.downloadProgress;
                yield return null;
            }
            
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error loading JSON: " + request.error);
                Data newData = new Data().Default(date);
                _data.SetData(newData);
                onLoaded?.Invoke();
            }
            else
            {
                var data = JsonUtility.FromJson<DateData>(request.downloadHandler.text);
                Data newData = new Data
                {
                    Date = data
                };
                _data.SetData(newData);
                onLoaded?.Invoke();
            }

            _loaderScreen.Progress = 1;
            LoadNetwork();
        }
    }
}