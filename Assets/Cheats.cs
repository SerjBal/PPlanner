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

            var channelList = new List<ItemData>();
            for (int i = 0; i < channelsCount; i++)
            {
                var channel = new ItemData();
                channel.Key = $"{i} channel";
                channel.Content = new List<ItemData>();
                if (isWithTime)
                {
                    var time = new ItemData();
                    time.Key = $"{12}:{i*2}";
                    channel.Content.Add(time);
                }
                channelList.Add(channel);
            }
            var currentDate = DateTime.Today;
            string date = $"{currentDate.Day}.{currentDate.Month}.{currentDate.Year}";
            Data.Value.DateItem = new ItemData
            {
                Key = date,
                Content = channelList
            };
            Saver.Save();
        }
    }
}
