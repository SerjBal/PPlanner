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
            await _assets.Load<GameObject>(Const.EditTimeWindow);
        }

        public void CreateEditWindow<T>(IHierarchical viewModel, string addressable) where T : IWindowViewModel, new()
        {
            if (new T() is EditWindow windowViewModel)
            {
                windowViewModel.Initialize(viewModel, _services);
                CreateWindowView(addressable, windowViewModel);
            }
        }

        public void CreateWarningWindow<T>(IWindowViewModel window, string path) where T : IWindowViewModel, new()
        {
            if (new T() is WarningWindow windowViewModel)
            {
                windowViewModel.Initialize(window, path);
                CreateWindowView(Const.WarningWindowPath, windowViewModel);
            }
        }

        private async void CreateWindowView(string path, IWindowViewModel viewModel)
        {
            var editView = await _assets.Instantiate<WindowView>(path, null);
            editView.Initialize(viewModel);
        }

        public async Task<T> CreateWindow<T>(string addressable, IHierarchical viewModel)
        {
            return await _assets.Instantiate<T>(addressable, null);
        }
    }
}