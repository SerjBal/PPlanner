using System;
using System.IO;
using UnityEngine;

namespace SerjBal
{
    public class DataProvider : IDataProvider
    {
        private readonly LoaderScreen _loaderScreen;

        public DataProvider(LoaderScreen loaderScreen) => _loaderScreen = loaderScreen;

        public DateTime CurrentDate { get; set; }

        public bool PathExists(string path) => 
            Directory.Exists(path);

        public string[] LoadDirectory(string path)
        {
            if (PathExists(path))
                return Directory.GetDirectories(path);
            return Array.Empty<string>();
        }

        public void CreateDirectory(string path)
        {
            if (!PathExists(path))
                Directory.CreateDirectory(path);
        }

        public void MoveDirectory(string oldPath, string newPath)
        {
            if (PathExists(oldPath))
            {
                DeleteDirectory(newPath);
                Directory.Move(oldPath, newPath);
                DeleteDirectory(oldPath);
            }
        }

        public void DeleteDirectory(string path)
        {
            if (PathExists(path))
                Directory.Delete(path, true);
        }

        public void CreateFile<T>(string path, T data)
        {
            try
            {
                var json = JsonUtility.ToJson(data);
                File.WriteAllText(path, json);
            }
            catch
            {
                Debug.LogError("file write error");
            }
        }

        public T LoadFile<T>(string path) where T : new()
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                return JsonUtility.FromJson<T>(json);
            }

            return new T();
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
                try
                {
                    File.Delete(path);
                }
                catch (IOException ex)
                {
                    Debug.LogError("file delete error");
                }
        }
    }
}