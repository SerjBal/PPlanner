using System;
using UnityEngine;

namespace SerjBal
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private CalendarView calendarView;
        [SerializeField] private SearchButtonView searchButtonView;
        [SerializeField] private GameObject dateContainer;
        [SerializeField] private GameObject blocker;
        private IAssetsProvider _assets;

        public CalendarView CalendarView => calendarView;
        public SearchButtonView GetSearchButtonView => searchButtonView;
        public GameObject GetContainer() => dateContainer;
        public GameObject GetBlocker() => blocker;

        public void Setup(MainMenuViewModel mainMenuVieModel, Services services, Configurations configs)
        {
            _assets = services.Single<IAssetsProvider>();
            
            mainMenuVieModel.onBlockerGet = GetBlocker;
            mainMenuVieModel.onContainerGet = GetContainer;

            CalendarView.Setup(new CalendarViewModel(services));
            var searchButton = new SearchButton();
            searchButton.Initialize(services);
            GetSearchButtonView.Setup(searchButton);
            CalendarView.Initialize();
            GetSearchButtonView.Initialize(configs.buttonConfigs);
        }

        private void OnApplicationQuit() => _assets.Cleanup();
    }
}