using System;
using System.Collections;
using System.IO;
using SerjBal.Code.Sources;
using UnityEngine;
using UnityEngine.Networking;

namespace SerjBal
{
    public class DataProvider : IDataProvider
    {
        public Data Data { get; private set; }

        private readonly ITemplatesProvider _templates;

        public DataProvider(ITemplatesProvider templates)
        {
            _templates = templates;
        }

        public IData GetDateData()
        {
            return Data.Date;
        }

        public bool DataHasKey(string channelKey, string timeKey)
        {
            bool value = false;
            if (Data.Date.Content!=null && Data.Date.Content.ContainsKey(channelKey))
            {
                var channelContent = Data.Date.Content[channelKey].Content;
                if (channelContent!=null && channelContent.ContainsKey(timeKey)) value = true;
            }
            return value;
        }

        public void SetData(Data data)
        {
            Data = data;
        }
    }
}