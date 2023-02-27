using UnityEngine;

namespace SerjBal
{
    public class MainMenuItemView : MonoBehaviour
    {
        //[SerializeField] private Transform highScreenContainer;
        public CalendarViewModel calendarViewModel;
        public SearchViewModel searchViewModel;
        public RectTransform dateContainer;
        public GameObject blocker;
        private Services _services;
        
        
        public void Initialize(Services servics, ButtonConfigs configs)
        {
            _services = servics;
            calendarViewModel.Initialize(servics);
            searchViewModel.Initialize(servics, configs);
        }
        
        public void UpdateMenu()
        {
            Destroy( dateContainer.GetChild(0).gameObject);
            var menuFactory = _services.Single<IMenuFactory>();
            menuFactory.CreateDateItem();
        }

        private void OnDestroy() => _services.Single<IAssetsProvider>().Cleanup();
    }
}