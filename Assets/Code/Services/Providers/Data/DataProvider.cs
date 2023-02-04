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

        public void SetData(Data data)
        {
            Data = data;
        }
    }
}