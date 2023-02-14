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
        public CalendarView calendarView;
        public RectTransform dateContainer;
        public GameObject blocker;
        private Services _services;
        
        
        public void Initialize(Services servics)
        {
            _services = servics;
            CalendarInitialize();
        }
        public void CalendarInitialize()
        {
            DateTimeFormatInfo dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            Calendar calendarData = new GregorianCalendar();
            
            int year = calendarData.GetYear(DateTime.Now);

            for (int month = 1; month <= calendarView.months.Length; month++)
            {
                var monthView = calendarView.months[month-1];
                monthView.nameText.text = dateTimeFormat.GetMonthName(month);

                int daysInMonth = calendarData.GetDaysInMonth(year, month);

                for (int day = 1; day < daysInMonth;)
                {
                    for (int j = 0; j < monthView.wheeks.Length;)
                    {
                        DateTime date = new DateTime(year, month, day);
                        string dayName = dateTimeFormat.GetDayName(date.DayOfWeek);
                        int dayOfWeekNumber = (int)date.DayOfWeek;
                        var wheek = monthView.wheeks[j];
                        var dayItem = wheek.days[dayOfWeekNumber];
                        dayItem.nameText.text = $"<size=100%>{day}<br><size=50%>{dayName.Substring(0, 3)}";
                        var dateString = $"{year}.{month}.{day}";
                        dayItem.button.onClick.AddListener(()=>LoadDate(dateString));
                        dayItem.SetState(GetDateState(year, month, day));

                        if (dayOfWeekNumber == 6) j += 1;
                        day++;
                        if (day >= daysInMonth)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private bool GetDateState(int year, int month, int day)
        {
            //check is date saved
            return false;
        }

        public void LoadDate(string date)
        {
            _services.Single<ISaveLoad>().Load(date);
            UpdateMenu();
        }
        
        public void UpdateMenu()
        {
            Destroy( dateContainer.GetChild(0).gameObject);
            var menuFactory = _services.Single<IMenuFactory>();
            menuFactory.CreateDateItem();
        }
    }
}