using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SerjBal
{
    public class Cheats : MonoBehaviour
    {
        public int channelsCount = 3;
        public bool isWithTime = false;
        
        public void SaveDate()
        {
            var services = new Services();
            var Data = services.Single<IDataProvider>();
            var Saver = services.Single<ISaveLoad>();

            Dictionary<string, IData> channelList = new Dictionary<string, IData>();
            for (int i = 0; i < channelsCount; i++)
            {
                var channel = new ChannelData();
                channel.Key = $"{i} channel";
                channel.Content = new Dictionary<string, IData>();
                if (isWithTime)
                {
                    var time = new TimeData();
                    time.Key = $"{12}:{i*2}";
                    channel.Content.Add(time.Key, time);
                }
                channelList.Add(channel.Key, channel);
            }
            var currentDate = DateTime.Today;
            string date = $"{currentDate.Day}.{currentDate.Month}.{currentDate.Year}";
            Data.Value.Date = new DateData
            {
                Key = date,
                Content = channelList
            };
            Saver.Save();
        }
    }
}
