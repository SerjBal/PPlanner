using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace SerjBal
{
    public class CalendarView : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown yearDropdown;
        [SerializeField] private MonthView[] months;
        private Calendar _calendarData;
        private DateTimeFormatInfo _dateTimeFormat;
        public IsDateExists OnDateCheck { get; set; }
        public LoadData OnLoadDate { get; set; }

        private void Awake()
        {
            _calendarData = new GregorianCalendar();
            _dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
        }

        public void Initialize()
        {
            Generate(_calendarData.GetYear(DateTime.Now));
        }

        private void Generate(int year)
        {
            var date = new DateTime(year, 1, 1).AddDays(-1);
            for (var month = 0; month < months.Length; month++)
            {
                var monthView = months[month];

                for (var j = 0; j < monthView.wheeks.Length;)
                {
                    date = date.AddDays(1);
                    var daysInMonth = _calendarData.GetDaysInMonth(date.Year, date.Month);
                    var dayOfWeek = (int)date.DayOfWeek;
                    var wheek = monthView.wheeks[j];
                    var dayItem = wheek.days[dayOfWeek];
                    var dayName = $"<size=100%>{date.Day}<br><size=50%>" +
                                  $"{_dateTimeFormat.GetDayName(date.DayOfWeek).Substring(0, 3)}";

                    dayItem.nameText.text = dayName;
                    var dateCopy = date;
                    dayItem.button.onClick.AddListener(() => OnLoadDate(dateCopy));
                    dayItem.SetState(OnDateCheck(date));

                    if (dayOfWeek == 6) j += 1;
                    if (date.Day == daysInMonth) break;
                }
                monthView.nameText.text = _dateTimeFormat.GetMonthName(date.Month);
            }
        }

        public delegate bool IsDateExists(DateTime date);

        public delegate void LoadData(DateTime data);
    }
}