using System.Collections.Generic;
using UnityEngine;

namespace SerjBal
{
    public class MainMenuViewModel : IHierarchical
    {
        public delegate GameObject GetObject();

        public GetObject onBlockerGet;
        public GetObject onContainerGet;

        public MainMenuViewModel()
        {
            ItemType = MenuItemType.None;
        }

        public GameObject Blocker
        {
            get => onBlockerGet.Invoke();
            set { }
        }

        public string Path { get; set; }
        public MenuItemType ItemType { get; set; }
        public IHierarchical Parent { get; set; }
        public List<IHierarchical> ChildList { get; set; }

        public Transform ContentContainer
        {
            get => onContainerGet.Invoke().transform;
            set { }
        }

        public void Block(bool isTrue)
        {
            Blocker.SetActive(isTrue);
        }
    }
}