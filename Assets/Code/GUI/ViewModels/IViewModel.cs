using System.Threading.Tasks;
using UnityEngine;

namespace SerjBal
{
    public interface IViewModel
    {
        public Transform ViewTransform { get; }
        public string Key { get; set; }
        void Add(IViewModel prefab);
        void Remove();
        void OnExpand();
        void OnCollapsed();
        void Initialize(ButtonConfigs configs);
    }
}