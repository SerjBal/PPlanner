using System;
using System.Collections.Generic;

namespace SerjBal
{
    public class AppStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public AppStateMachine(
            Configurations configurations,
            FadeScreen fadeScreen,
            LoaderScreen loaderScreen,
            Services services)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootloaderState)] = new BootloaderState(this, configurations, services, fadeScreen, loaderScreen),
                [typeof(GUIState)] = new GUIState(this, services, fadeScreen, loaderScreen),
                [typeof(TutorialState)] = new TutorialState(this)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            ChangeState<TState>().Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            (_activeState as IExitableState)?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}