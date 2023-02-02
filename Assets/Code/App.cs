using SerjBal.Code.Sources;

namespace SerjBal
{
    public class App
    {
        public AppStateMachine StateMachine;

        public App(ICoroutineRunner coroutineRunner, Configurations configurations, FadeScreen fadeScreen,
            LoaderScreen loaderScreen)
        {
            StateMachine = new AppStateMachine(coroutineRunner, configurations, fadeScreen, loaderScreen, Services.Container);
        }
    }
}