using System;
using System.Threading.Tasks;
using SerjBal.Code.Sources;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace SerjBal
{
    public class BootloaderState : IState
    {
        private readonly AppStateMachine _stateMachine;
        private readonly Services _services;
        private ICoroutineRunner _coroutineRunner;
        private readonly FadeScreen _fadeScreen;
        private readonly LoaderScreen _loaderScreen;
        private ISaveLoad _saveLoad;
        private Configurations _configurations;
        private ISaveLoad _dataLoader;

        public BootloaderState(AppStateMachine stateMachine, Configurations configurations, Services services, ICoroutineRunner coroutineRunner,
             FadeScreen fadeScreen, LoaderScreen loaderScreen)
        {
            _coroutineRunner = coroutineRunner;
            _stateMachine = stateMachine;
            _configurations = configurations;
            _services = services;
            _fadeScreen = fadeScreen;
            _loaderScreen = loaderScreen;

            RegisterServices();
        }

        public void Enter()
        {
            _fadeScreen.Show(true);
            var date = GetCurrentDate();
            _dataLoader.Load(date, OnLoaded);
        }

        private string GetCurrentDate()
        {
            var currentDate = DateTime.Today;
            string date = $"{currentDate.Year}.{currentDate.Month}.{currentDate.Day}";
            return date;
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<GUIState>();
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<ITemplatesProvider>(new TemplatesProvider(_coroutineRunner));
            _services.RegisterSingle<IDataProvider>(new DataProvider( _services.Single<ITemplatesProvider>()));
            _services.RegisterSingle<IAssetsProvider>(new AssetProvider());
            _services.RegisterSingle<ISaveLoad>(new SaveLoad(_coroutineRunner,_services.Single<IDataProvider>(), _loaderScreen));
            _services.RegisterSingle<IWindowsFactory>(new WindowsFactory(_services.Single<IAssetsProvider>()));
            _services.RegisterSingle<IMenuFactory>(new MenuFactory(_services.Single<IAssetsProvider>(),_services.Single<IDataProvider>(), _configurations));
            _services.RegisterSingle<IGUIModelView>(new GUIModelView());
            _dataLoader = _services.Single<ISaveLoad>();
            _services.Single<IWindowsFactory>().WarmUp();
            _services.Single<IMenuFactory>().WarmUp();
            _services.Single<IAssetsProvider>().Initialize();
            _dataLoader.Initialize();
        }
    }

    public class WindowsFactory : IWindowsFactory
    {
        private IAssetsProvider _assets;

        public WindowsFactory(IAssetsProvider assets)
        {
            _assets = assets;
        }
        
        public async Task WarmUp()
        {
            
            await _assets.Load<GameObject>(Const.EditWindowPath);
            await _assets.Load<GameObject>(Const.NewChannelWindowPath);
            await _assets.Load<GameObject>(Const.NewTimeWindowPath);
            await _assets.Load<GameObject>(Const.WarningWindowPath);
        }
        
         public async Task CreateEditDateWindow(IMenuItem menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.EditWindowPath, null);
            window.SetEditFormatText(Const.EditDateWindowFormatText);
            window.SetAcceptButtonText(Const.EditDateWindowButtonText);
            window.Initialize(menuItem);
        }

        public async Task CreateEditChannelWindow(IMenuItem menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.EditWindowPath, null);
            window.SetEditFormatText(Const.EditChannelWindowFormatText);
            window.SetAcceptButtonText(Const.EditChannelWindowButtonText);
            window.Initialize(menuItem);
        }
        
        public async Task CreateNewChannelWindow(IMenuItem menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.NewChannelWindowPath, null);
            window.SetEditFormatText(Const.NewChannelWindowFormatText);
            window.SetAcceptButtonText(Const.NewChannelWindowButtonText);
            window.Initialize(menuItem);
        }

        public async Task CreateEditTimeWindow(IMenuItem menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.EditWindowPath, null);
            window.SetEditFormatText(Const.EditTimeWindowFormatText);
            window.SetAcceptButtonText(Const.EditTimeWindowButtonText);
            window.Initialize(menuItem);
        }
        
        
        public async Task CreateNewTimeWindow(IMenuItem menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.NewTimeWindowPath, null);
            window.SetEditFormatText(Const.NewTimeWindowFormatText);
            window.SetAcceptButtonText(Const.NewTimeWindowButtonText);
            window.Initialize(menuItem);
        }

        public async Task CreateReplacingDataWindow(UnityAction onAccept)
        {
            var window = await _assets.Instantiate<IWarningWindow>(Const.WarningWindowPath, null);
            window.SetWarningText(Const.ReplaceWarningText);
            window.SetAcceptButtonText(Const.ReplaceButtonText);
            window.Initialize(onAccept);
        }

        public async Task<TextStyleLinkWindow> CreateTextLinkStyleWindow()
        {
            Debug.Log("link window");
            return null;
        }

        public Task<CreateTextColorWindow> CreateTextColorWindow()
        {
            Debug.Log("color window");
            return null;
        }
    }
}