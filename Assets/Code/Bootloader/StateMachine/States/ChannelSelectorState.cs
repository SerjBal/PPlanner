namespace SerjBal
{
    public class ChannelSelectorState : IState
    {
        private readonly AppStateMachine _stateMachine;
        private readonly Services _services;

        public ChannelSelectorState(AppStateMachine stateMachine, Services services)
        {
            _stateMachine = stateMachine;
            _services = services;
        }

        public void Enter()
        {
           
        }
        
        public void Exit()
        {
            
        }
    }
}