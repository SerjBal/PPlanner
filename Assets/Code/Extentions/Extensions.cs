using System.Collections.Generic;
using TMPro;
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
        
        public static string GetHexColor(this Color color)
        {
            int r = Mathf.RoundToInt(color.r * 255.0f);
            int g = Mathf.RoundToInt(color.g * 255.0f);
            int b = Mathf.RoundToInt(color.b * 255.0f);
            return string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b);
        }
    }
}