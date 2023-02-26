using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SerjBal
{
    public static class OtherExtensions
    { 
        public static void Clear(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
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

        public static int[] ToIntArray(this string s)
        {
            var keyParts = s.Split(".");
            var date = new int[keyParts.Length];
            for (int i = 0; i < keyParts.Length; i++)
            {
                date[i] = int.Parse(keyParts[i]);
            }
            return date;
        }
    }
}