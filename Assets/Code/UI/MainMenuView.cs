using System;
using UnityEngine;

namespace SerjBal
{
    public class MainMenuView : MonoBehaviour
    {
        public CalendarView calendarView;
        public SearchButtonView searchView;
        public SettingsButtonView settingsView;
        public GameObject dateContainer;
        public GameObject blocker;
        public Action CleanUp { private get; set; }
        private void OnApplicationQuit() => CleanUp?.Invoke();
    }
}