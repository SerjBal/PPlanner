using System;
using System.Collections.Generic;
using UnityEngine;

namespace SerjBal
{
    public abstract class ButtonViewModel
    {
        private bool _isCanvasActive;
        private string _path;
        public MenuItemType ItemType { get; set; }
        public IHierarchical Parent { get; set; }
        public List<IHierarchical> ChildList { get; set; }
        public Transform ContentContainer { get; set; }

        public ICommand RemoveCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand ContentUpdateCommand { get; set; }
        public ICommand AddNewContentCommand { get; set; }
        public ICommand CollapseFinishEnd { get; set; }
        public ICommand CollapseStartCommand { get; set; }
        public ICommand ExpandEndCommand { get; set; }
        public ICommand ExpandStartCommand { get; set; }

        public Action<string> OnKeyChanged { get; set; }
        public Action<bool> OnExpandStateChanged { get; set; }
        public Action<bool> OnCanvasChanged { get; set; }
        
        public bool IsSelected { get; private set; }
        public bool IsCanvasActive
        {
            get => _isCanvasActive;
            set
            {
                _isCanvasActive = value;
                OnCanvasChanged?.Invoke(value);
            }
        }

        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                OnKeyChanged?.Invoke(value);
            }
        }

        public abstract void Initialize(Services services);

        public void PushButton()
        {
            IsSelected = !IsSelected;
            OnExpandStateChanged?.Invoke(IsSelected);
        }
    }
}