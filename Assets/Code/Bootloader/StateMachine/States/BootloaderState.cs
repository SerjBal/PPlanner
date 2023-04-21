using SerjBal.Indication;
using SerjBal.Searching;

namespace SerjBal
{
    public class BootloaderState : IState
    {
        private readonly Configurations _configurations;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly FadeScreen _fadeScreen;
        private readonly LoaderScreen _loaderScreen;
        private readonly Services _services;
        private readonly AppStateMachine _stateMachine;

        public BootloaderState(AppStateMachine stateMachine, Configurations configurations, Services services,
            FadeScreen fadeScreen, LoaderScreen loaderScreen)
        {
            _stateMachine = stateMachine;
            _configurations = configurations;
            _services = services;
            _fadeScreen = fadeScreen;
            _loaderScreen = loaderScreen;
        }

        public void Enter()
        {
            RegisterServices();
            _fadeScreen.Show(true);
            _stateMachine.Enter<GUIState>();
        }

        private void RegisterServices()
        {
            var dataProvider = new DataProvider(_loaderScreen);
            _services.RegisterSingle<IDataProvider>(dataProvider);
            _services.RegisterSingle<ISearchingEngine>(new SearchingEngine(dataProvider));
            _services.RegisterSingle<IPostIndicator>(new Indication.PostIndicator(dataProvider));
            _services.RegisterSingle<ITemplatesProvider>(new TemplatesProvider(dataProvider));
            _services.RegisterSingle<IAssetsProvider>(new AssetProvider());
            _services.RegisterSingle<IWindowsFactory>(new WindowsFactory(_services.Single<IAssetsProvider>()));
            _services.RegisterSingle<IMenuFactory>(new MenuFactory(_services, _configurations));
            _services.RegisterSingle<ISettingsProvider>(new SettingsProvider(dataProvider));
            _services.RegisterSingle<IGUI>(new GUI());

            _services.Single<IAssetsProvider>().Initialize();
            _services.Single<IWindowsFactory>().WarmUp();
            _services.Single<IMenuFactory>().WarmUp();
        }
    }
}