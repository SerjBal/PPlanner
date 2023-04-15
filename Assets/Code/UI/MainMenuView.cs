using System;
using UnityEngine;

namespace SerjBal
{
    public class MainMenuView : MonoBehaviour
    {
        public CalendarView calendarView;
        public SearchButtonView searchButtonView;
        public GameObject dateContainer;
        public GameObject blocker;
        public Action CleanUp { private get; set; }
        private void OnApplicationQuit() => CleanUp?.Invoke();
    }
}