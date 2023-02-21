using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;

namespace SerjBal
{
    public class MainMenuItemView : MonoBehaviour
    {
        //[SerializeField] private Transform highScreenContainer;
        public CalendarViewModel calendarViewModel;
        public RectTransform dateContainer;
        public SearchViewModel searchingViewModel;
        public GameObject blocker;
        private Services _services;
        
        
        public void Initialize(Services servics)
        {
            _services = servics;
            calendarViewModel.Initialize();
            searchingViewModel.Initialize();
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