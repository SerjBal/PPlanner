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

        public void Copy(string source, string target)
        {
            if (!Directory.Exists(target))
                CreateDirectory(target);
            if (!Directory.Exists(source))
                CreateDirectory(source);
            CopyDir(source, target);
        }

        public string[] LoadDirectory(string path)
        {
            if (PathExists(path))
            {
                var x = new DirectoryInfo(path).GetDirectories();
                return Directory.GetDirectories(path);
            }
            return Array.Empty<string>();
        }

        public void CreateDirectory(string path)
        {
            if (!PathExists(path))
                Directory.CreateDirectory(path);
            else
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path);
            }
        }

        public void MoveDirectory(string oldPath, string newPath)
        { 
            DeleteDirectory(newPath);
            if (PathExists(oldPath))
            {
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
            var parent = Path.GetDirectoryName(path);
            CreateDirectory(parent);
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
        
        private void CopyDir(string sourcePath, string targetPath)
        {
            var dir = new DirectoryInfo(sourcePath);
            DirectoryInfo[] dirs = dir.GetDirectories();

            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Name == Const.TextFileName)
                {
                    string targetFilePath = Path.Combine(targetPath, file.Name);
                    file.CopyTo(targetFilePath);
                }
            }

            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(targetPath, subDir.Name);
                Copy(subDir.FullName, newDestinationDir);
            }
        }
    }
}