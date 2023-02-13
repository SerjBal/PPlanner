using System;
using System.Collections.Generic;
using SerjBal;

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
    public string text;
}
