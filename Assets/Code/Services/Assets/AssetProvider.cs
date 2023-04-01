using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SerjBal
{
    public class AssetProvider : IAssetsProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCashe = new();

        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }

        public async Task<T> Load<T>(string address) where T : class
        {
            if (_completedCashe.TryGetValue(address, out var completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
                Addressables.LoadAssetAsync<T>(address),
                address);
        }

        public Task<GameObject> Instantiate(string address)
        {
            return Addressables.InstantiateAsync(address).Task;
        }

        public async Task<T> Instantiate<T>(string address, Transform parent)
        {
            var prefab = await Load<GameObject>(address);
            var instance = Object.Instantiate(prefab, parent);
            return instance.GetComponent<T>();
        }

        public void Cleanup()
        {
            foreach (var resourceHandles in _handles.Values)
            foreach (var handle in resourceHandles)
                Addressables.Release(handle);

            _completedCashe.Clear();
            _handles.Clear();
        }

        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle => { _completedCashe[cacheKey] = completeHandle; };

            AddHandle<T>(cacheKey, handle);

            return await handle.Task;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle handle) where T : class
        {
            if (!_handles.TryGetValue(key, out var resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }
    }
}