using System;
using System.Collections.Generic;
using UnityEngine;
using static System.IO.Path;

namespace SerjBal
{
    public abstract class SplitButtonPresenter
    {
        private bool _isOverrideSorting;
        private string _path;
        public MenuItemType ItemType { get; set; }
        public IHierarchical Parent { get; set; }

        public List<IHierarchical> ChildList { get; set; }
        public Transform ContentContainer { get; set; }

        public ICommand RemoveCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand ContentUpdateCommand { get; set; }
        public ICommand AddNewContentCommand { get; set; }
        public ICommand CollapseEndCommand { get; set; }
        public ICommand CollapseStartCommand { get; set; }
        public ICommand ExpandEndCommand { get; set; }
        public ICommand ExpandStartCommand { get; set; }
        public Action<string> OnKeyChanged { get; set; }
        public Action<bool> OnExpandStateChanged { get; set; }
        public Action<bool> OnOverrideSortingChanged { get; set; }
        
        public bool IsSelected { get; private set; }
        public bool IsOverrideSorting  { get => _isOverrideSorting;
            set
            {
                _isOverrideSorting = value;
                OnOverrideSortingChanged?.Invoke(value);
            }
        }

        public string Path { get => _path;
            set
            {
                _path = value;
                OnKeyChanged?.Invoke(value);
            }
        }
        
        public string ContentPath => Combine(_path, Const.ContentDirectory);

        public abstract void Initialize(ButtonView view, Services services);

        public void PushButton()
        {
            IsSelected = !IsSelected;
            OnExpandStateChanged?.Invoke(IsSelected);
        }

        protected void InitializeBaseView(ButtonView view, string name)
        {
            var config = Configurations.Instance.buttonConfig;

            view.nameText.text = name;
            view.canvas.overrideSorting = IsOverrideSorting;
            ContentContainer = view.contentContainer;
            ContentContainer.gameObject.SetActive(false);

            if (view.animator)
            {
                view.animator.Initialize(config.expandAnimationCurve);
                view.animator.onExpandStartEvent = () => ExpandStartCommand?.Execute(view.canvas);
                view.animator.onExpandEndEvent = () => ExpandEndCommand?.Execute();
                view.animator.onCollapseStartEvent = () => CollapseStartCommand?.Execute();
                view.animator.onCollapseEndEvent = () => CollapseEndCommand?.Execute(view.canvas);
                OnExpandStateChanged += value =>
                {
                    if (value)
                        view.animator.PlayOpen();
                    else
                        view.animator.PlayClose();
                };
            }

            if (view.controller)
            {
                view.controller.Initialize(config);
                view.editButton.onClick.AddListener(() => EditCommand?.Execute());
                view.removeButton.onClick.AddListener(() => RemoveCommand?.Execute());
                view.controller.onSelectedEvent = PushButton;
            }
            
            OnKeyChanged = (path) => view.nameText.text = GetFileName(path);
            OnOverrideSortingChanged = (value) => view.canvas.overrideSorting = value;
        }
    }
}