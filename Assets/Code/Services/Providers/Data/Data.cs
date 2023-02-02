using System;
using System.Collections.Generic;
using SerjBal;

public class Data
{
    public DateData Date;
}

[Serializable]
public class DateData : IData
{
    public Dictionary<string, IData> Content { get; set; }
}

[Serializable]
public class ChannelData : IData
{ 
    public Dictionary<string, IData> Content { get; set; }
}

[Serializable]
public class TimeData : IData
{
    public string Key { get; set; }
    public Dictionary<string, IData> Content { get; set; }
    public string value;
}