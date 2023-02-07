using System;
using System.Collections.Generic;
using SerjBal;

public class Data
{
    public DateData Date;

    public Data Default(string date)
    {
        return new Data
        {
            Date = new DateData
            {
                Key = date,
                Content = null
            }
        };
    }
}

[Serializable]
public class DateData : IData
{
    public string Key { get; set; }
    public Dictionary<string, IData> Content { get; set; }
}

[Serializable]
public class ChannelData : IData
{ 
    public Dictionary<string, IData> Content { get; set; }
    public string Key { get; set; }
}

[Serializable]
public class TimeData : IData
{
    public string Key { get; set; }
    public string TextKey { get; set; }
    public Dictionary<string, IData> Content { get; set; }
}

[Serializable]
public class TextData
{
    public string text;
}
