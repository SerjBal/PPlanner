using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SerjBal
{
    public class SearchEngine
    {
        private readonly IDataProvider _data;
        private Dictionary<string, PostData> _resultDataList;

        public SearchEngine(Services services) => 
            _data = services.Single<IDataProvider>();

        public Task<bool> Search(string goal, int daysRange = 0)
        {
            _resultDataList = new Dictionary<string, PostData>();
            var date = _data.CurrentDate;

            Search(goal, date);
            if (daysRange != 0)
                SearchAround(goal, daysRange, date);

            return Task.FromResult(_resultDataList.Count > 0);
        }

        private void Search(string goal, DateTime date)
        {
            var posts = GetTextList(date);

            foreach (var post in posts)
            {
                var goalExists = FindText(post.Value.text, goal);
                if (goalExists)
                    _resultDataList.Add(post.Key, post.Value);
            }
        }

        private void SearchAround(string goal, int daysRange, DateTime date)
        {
            for (var i = 0; i < daysRange; i++)
            {
                var nextDate = date.AddDays(i);
                var prevDate = date.AddDays(-i);
                Search(goal, nextDate);
                Search(goal, prevDate);
            }
        }

        private Dictionary<string, PostData> GetTextList(DateTime date)
        {
            var posts = new Dictionary<string, PostData>();
            var dateContentPath = Path.Combine(date.ToPath(), Const.ContentDrectory);
            foreach (var channelDir in _data.LoadDirectory(dateContentPath))
            foreach (var timeDir in _data.LoadDirectory(Path.Combine(channelDir, Const.ContentDrectory)))
            {
                var filePath = Path.Combine(timeDir, Const.TextFileName);
                var text = _data.LoadFile<PostData>(filePath);
                if (text.text is { Length: > 0 }) posts.Add(timeDir, text);
            }
            return posts;
        }
        
        private bool FindText(string text, string substring) => 
            text?.IndexOf(substring, StringComparison.OrdinalIgnoreCase) > -1;
        public Dictionary<string, PostData> GetResults() => _resultDataList;
    }
}