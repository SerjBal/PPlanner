using System.Threading.Tasks;
using UnityEngine;

namespace SerjBal
{
    public interface IAssetsProvider : IService
    {
        Task<GameObject> Instantiate(string path);
        Task<T> Instantiate<T>(string path, Transform parent);
        void Cleanup();
        Task<T> Load<T>(string address) where T : class;
        void Initialize();
    }
}