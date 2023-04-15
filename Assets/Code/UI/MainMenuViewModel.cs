using System.Collections.Generic;
using UnityEngine;

namespace SerjBal
{
    public class MainMenuViewModel : IHierarchical
    {
        public MainMenuViewModel(MainMenuView view, Services services)
        {
            ItemType = MenuItemType.None;
            
            var assets = services.Single<IAssetsProvider>();
            view.CleanUp = assets.Cleanup;
            
            Blocker = view.blocker;
            ContentContainer = view.dateContainer.transform;
            
            var searchButton = new SearchButton();
            var calendar = new CalendarViewModel();
            searchButton.Initialize(view.searchButtonView, services);
            calendar.Initialize(view.calendarView, services);
        }

        public GameObject Blocker { get; }
        public string Path { get; set; }
        public string ContentPath { get; }
        public MenuItemType ItemType { get; set; }
        public IHierarchical Parent { get; set; }
        public List<IHierarchical> ChildList { get; set; }
        public Transform ContentContainer { get;}

        public void Block(bool isTrue) => Blocker.SetActive(isTrue);
    }
}