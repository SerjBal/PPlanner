using System;
using System.Threading.Tasks;
using SerjBal.Windows;
using UnityEngine;

namespace SerjBal
{
    public class WindowsFactory : IWindowsFactory
    {
        private readonly IAssetsProvider _assets;
        private readonly Services _services;

        public WindowsFactory(IAssetsProvider assets)
        {
            _assets = assets;
            _services = new Services();
        }

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(Const.ItemNamingWindowPath);
            await _assets.Load<GameObject>(Const.WarningWindowPath);
            await _assets.Load<GameObject>(Const.EditDateWindowPath);
            await _assets.Load<GameObject>(Const.EditTimeWindowPath);
            await _assets.Load<GameObject>(Const.ColorSelectorWindowPath);
        }

        public async void CreateEditWindow<T>(IHierarchical viewModel, string addressable) where T : IWindowPresenter, new()
        {
            if (new T() is EditWindow windowPresenter)
            {
                var view = await CreateWindowView(addressable);
                windowPresenter.Initialize(viewModel, _services, view);
            }
        }

        public async void CreateWarningWindow<T>(IWindowPresenter window, string path) where T : IWindowPresenter, new()
        {
            if (new T() is WarningWindow windowPresenter)
            {
                var view = await CreateWindowView(Const.WarningWindowPath);
                windowPresenter.Initialize(window, path, view);
            }
        }

        public async void CreateColorSelectorWindow(Action action, ColorSettingType settingType)
        {
            var view = await CreateWindowView(Const.ColorSelectorWindowPath);
            var windowPresenter = new ColorSelectorWindow(action);
            windowPresenter.Initialize(settingType, _services, view);
            
        }
        
        private async Task<WindowView> CreateWindowView(string path)
        {
            return await _assets.Instantiate<WindowView>(path, null);
        }
    }
}