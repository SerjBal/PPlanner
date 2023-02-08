using System;
using System.Collections.Generic;
using SerjBal;

public class Data
{
    public ItemData DateItem;
}

[Serializable]
public class ItemData
{
    public string Key;
    public List<ItemData> Content;
}
