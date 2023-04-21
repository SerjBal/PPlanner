using System;
using System.Collections.Generic;
using System.IO;

namespace SerjBal.Indication
{
    public class PostIndicator : IPostIndicator
    {
        public bool IsUpdateable { get; private set; }
        private readonly IDataProvider _data;
        private DateTime _time;

        public PostIndicator(IDataProvider data)
        {
            _data = data;
            IsUpdateable = false;
        }

        public void Initialize(DateTime dateTime)
        {
            var currentTime = DateTime.Now;
            if (dateTime.Year <= currentTime.Year
                && dateTime.Month <= currentTime.Month
                && dateTime.Day < currentTime.Day)
                IsUpdateable = false;
            else
                IsUpdateable = true;
        }

        public List<PostState> GetPostsStates(string path)
        {
            var statesList = new List<PostState>();
            if (Directory.Exists(path))
            {
                var directories = Directory.GetDirectories(path);
                foreach (var directory in directories)
                {
                    path = Path.Combine(directory, Const.TypeFileName);
                    var type = _data.LoadFile<PostType>(path);
                    var metaData = new PostState
                    {
                        minute = Path.GetFileName(directory).ToMinutes(),
                        postType = type
                    };
                    statesList.Add(metaData);
                }
            }
            return statesList;
        }

        public void SavePostType(string path, PostType state)
        {
            var filePath = Path.Combine(path, Const.TypeFileName);
            _data.CreateFile(filePath, state);
        }
    }
}