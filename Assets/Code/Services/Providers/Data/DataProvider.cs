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
        private Data _data;
        
        private readonly ITemplatesProvider _templates;

        public DataProvider(ITemplatesProvider templates)
        {
            _templates = templates;
        }

        public DateData GetDateData()
        {
            DateData data = null;
            if (_data.Date==null)
            {
                if (_templates.HasTamplates()) data = _templates.SelectedTemplate;
            }
            else
            {
                data = _data.Date;
            }

            return data;
        }

        public void SetData(Data data)
        {
            _data = data;
        }
    }
}