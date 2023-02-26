using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace SerjBal
{
    public class CalendarViewModel : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown yearDropdown;
        [SerializeField] private MonthView[] months;
        private Services _services;
        private Calendar _calendarData;
        
         public void Initialize(Services services)
        {
            _calendarData = new GregorianCalendar();
            _services = services;
            
            DateTimeFormatInfo dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;

            int year = _calendarData.GetYear(DateTime.Now);
            for (int month = 1; month <= months.Length; month++)
            {
                var monthView = months[month-1];
                monthView.nameText.text = dateTimeFormat.GetMonthName(month);

                int daysInMonth = _calendarData.GetDaysInMonth(year, month);
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
                        dayItem.SetState(IsDateExists(year, month, day));

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

        private bool IsDateExists(int year, int month, int day)
        {
            _services.Single<ISaveLoad>().Exists($"{year}{month}{day}");
            return false;
        }

        public void LoadDate(string date)
        {
            var saveload = _services.Single<ISaveLoad>();
            saveload.Save();
            saveload.Load(date, null);
            _services.Single<IGUIModelView>().UpdateMenu();
        }
    }
}
