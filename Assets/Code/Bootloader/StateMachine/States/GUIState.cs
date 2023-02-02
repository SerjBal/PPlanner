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

        public void Enter()
        {
            _GUI = _services.Single<IGUIModelView>();
            _GUI.Initialize();
        }

        public void Exit()
        {
            _fadeScreen.Show(false);
        }
    }
}
    