using System;

namespace SerjBal
{
    public class GUIState : IState
    {
        private readonly FadeScreen _fadeScreen;
        private readonly Services _services;
        private readonly AppStateMachine _stateMachine;
        private ButtonConfigs _buttonConfigs;
        private IDataProvider _data;
        private IGUI _iguiView;
        private ITemplatesProvider _templates;

        public GUIState(AppStateMachine stateMachine, Services services,
            FadeScreen fadeScreen)
        {
            _stateMachine = stateMachine;
            _services = services;
            _fadeScreen = fadeScreen;
        }

        public async void Enter()
        {
            var data = _services.Single<IDataProvider>();
            data.CurrentDate = DateTime.Today;

            var guiService = _services.Single<IGUI>();
            await guiService.Initialize(_services);
            guiService.UpdateMenu();

            _stateMachine.Enter<TutorialState>();
        }

        public void Exit()
        {
            _fadeScreen.Show(false);
        }
    }
}