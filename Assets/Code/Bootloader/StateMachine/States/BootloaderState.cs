using System;
using SerjBal.Code.Sources;
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
        }
    }
}