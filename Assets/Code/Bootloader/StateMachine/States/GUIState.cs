using System;
using SerjBal.Indication;

namespace SerjBal
{
    public class GUIState : IState, IExitableState
    {
        private readonly FadeScreen _fadeScreen;
        private readonly LoaderScreen _loaderScreen;
        private readonly Services _services;
        private readonly AppStateMachine _stateMachine;
        private ButtonConfig _buttonConfig;
        private IDataProvider _data;
        private IGUI _iguiView;
        private ITemplatesProvider _templates;

        public GUIState(AppStateMachine stateMachine, Services services,
            FadeScreen fadeScreen, LoaderScreen loaderScreen)
        {
            _stateMachine = stateMachine;
            _services = services;
            _fadeScreen = fadeScreen;
            _loaderScreen = loaderScreen;
        }

        public async void Enter()
        {
            var currentDate = DateTime.Today;
            _services.Single<IDataProvider>().CurrentDate = currentDate;
            _services.Single<IPostIndicator>().Initialize(currentDate);

            var guiService = _services.Single<IGUI>();
            await guiService.Initialize(_services);
            guiService.UpdateMenu();

            _stateMachine.Enter<TutorialState>();
        }

        public void Exit()
        {
            _fadeScreen.Show(false);
            _loaderScreen.Show(false);
        }
    }
}