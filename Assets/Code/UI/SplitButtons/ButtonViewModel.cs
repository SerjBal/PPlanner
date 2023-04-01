using System;
using System.Collections.Generic;
using UnityEngine;

namespace SerjBal
{
    public abstract class ButtonViewModel
    {
        private bool _isCanvasActive;
        private bool _isSelected;
        private string _path;
        public MenuItemType ItemType { get; set; }
        public IHierarchical Parent { get; set; }
        public List<IHierarchical> ChildList { get; set; }
        public Transform ContentContainer { get; set; }

        public bool IsCanvasActive
        {
            get => _isCanvasActive;
            set
            {
                _isCanvasActive = value;
                OnCanvasChanged?.Invoke(value);
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnExpandStateChanged?.Invoke(value);
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

        public ICommand SelectCommand { get; set; }
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
        
        public virtual void Initialize(Services services){}
    }
}