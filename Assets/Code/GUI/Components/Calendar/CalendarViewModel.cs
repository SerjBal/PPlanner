using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class CalendarViewModel : MonoBehaviour
    {
        public TMP_Dropdown yearDropdown;
        public ScrollRect scrollRect;
        public MonthView[] months;
        private Services _services;
        
         public void Initialize()
        {
            _services = new Services();
            
            DateTimeFormatInfo dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            Calendar calendarData = new GregorianCalendar();
            
            int year = calendarData.GetYear(DateTime.Now);

            for (int month = 1; month <= months.Length; month++)
            {
                var monthView = months[month-1];
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
            var saveload = _services.Single<ISaveLoad>();
            saveload.Save();
            saveload.Load(date);
            _services.Single<IGUIModelView>().UpdateMenu();
        }
    }
}
