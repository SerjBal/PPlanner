namespace SerjBal
{
    public class App
    {
        public AppStateMachine StateMachine;

        public App(Configurations configurations, FadeScreen fadeScreen,
            LoaderScreen loaderScreen)
        {
            StateMachine = new AppStateMachine(configurations, fadeScreen, loaderScreen, Services.Container);
        }
    }
}