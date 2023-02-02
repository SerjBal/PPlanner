using SerjBal.Code.Sources;

namespace SerjBal
{
    public class SearchState: IState
    {
        private readonly AppStateMachine _stateMachine;
        private readonly Configurations _configurations;
        private readonly Services _services;
        private readonly IDataProvider _data;
        private readonly IGUIModelView _GUI;

        public SearchState(AppStateMachine stateMachine, Configurations configurations, Services services)
        {
            _stateMachine = stateMachine;
            _configurations = configurations;
            _services = services;
            _data = _services.Single<IDataProvider>();
            
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            
        }
    }
}