using System.Collections.Generic;
using UnityEngine;

namespace SerjBal
{
    public static class Extensions
    {
        public static void Add(this Transform container, GameObject gameObject)
        {
            gameObject.transform.SetParent(container, false);
        }
        
        public static bool ContainsKey(this  List<ItemData> content, string key)
        {
            foreach (ItemData item in content)
            {
                if (item.Key==key) return true;
            }
            return false;
        }

        public static ItemData Get(this  List<ItemData> content, string key)
        {
            foreach (ItemData item in content)
            {
                if (item.Key==key) return item;
            }
            return null;
        }
        
        public static void RemoveKey(this  List<ItemData> content, string key)
        {
            for (int i = 0; i < content.Count; i++)
            {
                if (content[i].Key==key) content.RemoveAt(i);
            }
        }
    }
}