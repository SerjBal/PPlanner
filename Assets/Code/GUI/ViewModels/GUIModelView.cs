using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SerjBal
{
    public class GUIModelView : IGUIModelView
    {
        private MainMenuItemView _GUI;

        public async Task Initialize(MainMenuItemView gui) => _GUI = gui;

        public float GetMenuBounds() => _GUI.dateContainer.rect.height;

        public void UpdateMenu() => _GUI.UpdateMenu();

        public void DisableMenuInteracton(bool isTrue) => _GUI.blocker.SetActive(isTrue);

        public void EnableCalendar(bool isTrue) => _GUI.calendarViewModel.gameObject.SetActive(isTrue);
    }
}