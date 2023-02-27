using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace SerjBal
{
    public class SearchEngine
    { 
        private IDataProvider _data;
        private ISaveLoad _saveLoad;
        private List<TextData> _posts;
        private List<TextData> _resultDataList;
        private GregorianCalendar _gregCalendar;

        public SearchEngine(Services services)
        {
            _saveLoad = services.Single<ISaveLoad>();
            _data = services.Single<IDataProvider>();
            _gregCalendar = new GregorianCalendar();
        }

        public List<TextData> GetResults() => _resultDataList;

        public bool FindText(string text, string substring) => 
            text?.IndexOf(substring, StringComparison.OrdinalIgnoreCase) > -1;

        public Task<bool> Search(string goal, int daysRange = 0)
        {
            int[] date = _data.Value.DateItem.Key.ToIntArray();
            
            Search(goal, date);
            if (daysRange != 0)
            {
                SearchAround(goal, daysRange, date);
            }

            return Task.FromResult(_resultDataList.Count > 0);
        }

        private void Search(string goal, int[] date)
        {
            int year = date[0]; int month = date[1]; int day = date[2];
            GetPostsByDate($"{year}.{month}.{day}");
            
            for (int i = 0; i < _posts.Count; i++)
            {
                bool goalExists = FindText(_posts[i].text, goal);
                if (goalExists)
                {
                    _resultDataList.Add(_posts[i]);
                }
            }
        }
        
        private void SearchAround(string goal, int daysRange, int[] date)
        {
            int[] nextDate = date;
            int[] prevDate = date;
            for (int i = 0; i < daysRange; i++)
            {
                nextDate = GetNextDate(nextDate);
                prevDate = GetPrevDate(prevDate);

                Search(goal, nextDate);
                Search(goal, prevDate);
            }
        }

        private void GetPostsByDate(string dateKey)
        {
            _posts = new List<TextData>();
            _resultDataList = new List<TextData>();
            var data = _saveLoad.Load(dateKey);
            if (data!=null)
            {
                for (int c = 0; c < data.Content.Count; c++)
                {
                    for (int t = 0; t < data.Content[c].Content.Count; t++)
                    {
                        var channel = data.Content[c];
                        var time = channel.Content[t];
                        var textKey = time.Content[0].Key;
                        var date = data.Key.ToIntArray();

                        var textData = new TextData();
                        textData.year = date[0];
                        textData.month = date[1];
                        textData.day = date[2];
                        textData.channel = channel.Key;
                        textData.time = time.Key;
                        textData.text = _saveLoad.LoadText(textKey).text;
                        _posts.Add(textData);
                    }
                }
            }
        }
        
        private int[] GetPrevDate(int[] date)
        {
            int year = date[0]; int month = date[1]; int day = date[2];
            int maxMonth = 11;
            int prevDay = day - 1;
            if (prevDay < 0)
            {
                if (month == 0)
                {
                    month = maxMonth;
                    year -= 1;
                }
                else
                {
                    month -= 1;
                }
                int maxDay = _gregCalendar.GetDaysInMonth(year, month) - 1;
                prevDay = maxDay;
            }
            return new int[]{year, month, prevDay};
        }

        private int[] GetNextDate(int[] date)
        {
            int year = date[0]; int month = date[1]; int day = date[2];
            int maxDay = _gregCalendar.GetDaysInMonth(year, month) - 1;
            int maxMonth = 11;
            int nextDay = day + 1;
            if (nextDay > maxDay)
            {
                nextDay = 0;
                if (month == maxMonth)
                {
                    month = 0;
                    year += 1;
                }
                else
                {
                    month += 1;
                }
            }
            return new int[]{year, month, nextDay};
        }
    }
}