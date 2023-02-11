using System;
using System.Globalization;
using System.Threading.Tasks;
using SerjBal.Code.Sources;

namespace SerjBal
{
    public class GUIState : IState
    {
        private readonly AppStateMachine _stateMachine;
        private readonly Services _services;
        private readonly FadeScreen _fadeScreen;
        private IGUIModelView _GUI;
        private ITemplatesProvider _templates;
        private readonly IDataProvider _data;

        public GUIState(AppStateMachine stateMachine, Services services, FadeScreen fadeScreen)
        {
            _stateMachine = stateMachine;
            _services = services;
            _fadeScreen = fadeScreen;
        }

        public async void Enter()
        {
            var factory = _services.Single<IAppFactory>();
            var guiModelView = _services.Single<IGUIModelView>();
            
            var gui = await factory.CreateGUI();
            gui.Initialize(_services);
            guiModelView.Initialize(gui);
            await factory.CreateDateItem();
            
            _stateMachine.Enter<SearchState>();
        }

        public void Exit()
        {
            _fadeScreen.Show(false);
        }
    }
}
    