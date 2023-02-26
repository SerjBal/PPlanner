using System;
using System.Globalization;
using System.Threading.Tasks;
using SerjBal.Code.Sources;

namespace SerjBal
{
    public class GUIState : IState
    {
        private readonly AppStateMachine _stateMachine;
        private readonly Configurations _configurations;
        private readonly Services _services;
        private readonly FadeScreen _fadeScreen;
        private IGUIModelView _GUI;
        private ITemplatesProvider _templates;
        private IDataProvider _data;
        private ButtonConfigs _buttonConfigs;

        public GUIState(AppStateMachine stateMachine, Configurations configurations, Services services,
            FadeScreen fadeScreen)
        {
            _stateMachine = stateMachine;
            _configurations = configurations;
            _services = services;
            _fadeScreen = fadeScreen;
        }

        public async void Enter()
        {
            var factory = _services.Single<IMenuFactory>();
            var guiModelView = _services.Single<IGUIModelView>();
            
            var gui = await factory.CreateGUI();
            gui.Initialize(_services, _configurations.buttonConfigs);
            guiModelView.Initialize(gui);
            await factory.CreateDateItem();
            
            _stateMachine.Enter<LoopState>();
        }

        public void Exit()
        {
            _fadeScreen.Show(false);
        }
    }
}
    