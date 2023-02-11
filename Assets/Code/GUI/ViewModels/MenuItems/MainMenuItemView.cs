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
        public CanvasGroup canvasGroup;
        
        
        public void Initialize()
        {
            CalendarInitialize();
        }
        public void CalendarInitialize()
        {
            DateTimeFormatInfo dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            Calendar calendarData = new GregorianCalendar();
            
            int year = calendarData.GetYear(DateTime.Now);

            for (int i = 0; i < calendarView.months.Length; i++)
            {
                var monthView = calendarView.months[i];
                monthView.nameText.text = dateTimeFormat.GetMonthName(i + 1);

                int daysInMonth = calendarData.GetDaysInMonth(year, i + 1);

                for (int day = 1; day < daysInMonth;)
                {
                    for (int j = 0; j < monthView.wheeks.Length;)
                    {
                        DateTime date = new DateTime(year, i + 1, day);
                        string dayName = dateTimeFormat.GetDayName(date.DayOfWeek);
                        int dayOfWeekNumber = (int)date.DayOfWeek;
                        var wheek = monthView.wheeks[j];
                        wheek.days[dayOfWeekNumber].nameText.text = $"<size=100%>{day}<br><size=50%>{dayName}";

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
    }
}