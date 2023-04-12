using System.Collections.Generic;
using UnityEngine;

namespace SerjBal
{
    public interface IHierarchical
    {
        MenuItemType ItemType { get; set; }
        string Path { get; set; }
        string ContentPath { get; }
        IHierarchical Parent { get; set; }
        List<IHierarchical> ChildList { get; set; }
        Transform ContentContainer { get; set; }
    }
    
    public enum MenuItemType
    {
        None,
        Date,
        Channel,
        Time
    }
}