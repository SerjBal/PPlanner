using UnityEngine;

namespace SerjBal
{
    public class Bootloader : MonoBehaviour, ICoroutineRunner
    {
        public FadeScreen fadeScreen;
        public LoaderScreen loaderScreen;
        public Configurations configurations;
        private App _app;

        private void Awake()
        {
            fadeScreen.Initialize();
            loaderScreen.Initialize();
            _app = new App(configurations, fadeScreen, loaderScreen);
            _app.StateMachine.Enter<BootloaderState>();
            DontDestroyOnLoad(this);
        }
    }
}