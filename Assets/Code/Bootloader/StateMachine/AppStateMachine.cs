using System;
using System.Collections.Generic;
using SerjBal.Code.Sources;

namespace SerjBal
{
    public class AppStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public AppStateMachine(ICoroutineRunner coroutineRunner,
            Configurations configurations,
            FadeScreen fadeScreen,
            LoaderScreen loaderScreen,
            Services services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootloaderState)] = new BootloaderState( this, configurations, services, coroutineRunner, fadeScreen, loaderScreen),
                [typeof(GUIState)] = new GUIState(this, services, fadeScreen),
                [typeof(SearchState)] = new SearchState(this, configurations, services),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            ChangeState<TState>().Enter();
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}