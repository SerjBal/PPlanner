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
        }

        public async void CreateEditWindow<T>(IHierarchical viewModel, string addressable) where T : IWindowViewModel, new()
        {
            if (new T() is EditWindow windowViewModel)
            {
                var view = await CreateWindowView(addressable);
                windowViewModel.Initialize(viewModel, _services, view);
            }
        }

        public async void CreateWarningWindow<T>(IWindowViewModel window, string path) where T : IWindowViewModel, new()
        {
            if (new T() is WarningWindow windowViewModel)
            {
                var view = await CreateWindowView(Const.WarningWindowPath);
                windowViewModel.Initialize(window, path, view);
            }
        }

        public async Task<WindowView> CreateWindowView(string path)
        {
            return await _assets.Instantiate<WindowView>(path, null);
        }
    }
}