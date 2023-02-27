using System;
using System.Collections.Generic;

public class Data
{
    public ItemData DateItem = new ItemData();
}

[Serializable]
public class ItemData
{
    public string Key;
    public List<ItemData> Content;
}

[Serializable]
public class TextData
{
    public int year;
    public int month;
    public int day;
    public string channel;
    public string time;
    public string text;
}
