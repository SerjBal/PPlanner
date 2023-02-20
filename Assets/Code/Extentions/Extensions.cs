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

        public static void Clear(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }
        public static bool ContainsKey(this List<ItemData> content, string key)
        {
            foreach (ItemData item in content)
            {
                if (item.Key==key) return true;
            }
            return false;
        }

        public static ItemData Get(this List<ItemData> content, string key)
        {
            foreach (ItemData item in content)
            {
                if (item.Key==key) return item;
            }
            return null;
        }
        
        public static void Override(this List<ItemData> content, string key, ItemData data)
        {
            bool keyExists = false;
            for (int i = 0; i < content.Count; i++)
            {
                if (content[i].Key == key)
                {
                    content[i] = data;
                    keyExists = true;
                }
            }
            if (!keyExists) content.Add(data);
        }
        
        public static void RemoveKey(this List<ItemData> content, string key)
        {
            for (int i = 0; i < content.Count; i++)
            {
                if (content[i].Key==key) content.RemoveAt(i);
            }
        }
        
        public static void RemoveKey(this List<IMenuItem> content, string key)
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
        
        public static string GetKeyPath(this IMenuItem item)
        {
            switch (item)
            {
                case DateMenuItem dateMenuItem:
                    return item.Key;
                case ChannelMenuItem channelMenuItem:
                    return $"{item.Parent.Key}/{item.Key}";
                case TimeMenuItem timeMenuItem:
                    return $"{item.Parent.Parent.Key}/{item.Parent.Key}/{item.Key}";
                default:
                    Debug.Log("Error");
                    return null;
            }
        }
    }
}