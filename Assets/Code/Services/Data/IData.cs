using System.Collections.Generic;

namespace SerjBal
{
    public interface IData
    {
        public Dictionary<string, IData> Content { get; set; }
        string Key { get; set; }
    }
}