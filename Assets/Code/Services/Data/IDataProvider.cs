using System;

namespace SerjBal
{
    public interface IDataProvider : IService
    {
        DateTime CurrentDate { get; set; }
        string[] LoadDirectory(string path);
        void CreateDirectory(string path);
        void MoveDirectory(string oldPath, string newPath);
        void DeleteDirectory(string path);
        void CreateFile<T>(string path, T data);
        T LoadFile<T>(string path) where T : new();
        void DeleteFile(string path);
        bool PathExists(string path);
    }
}