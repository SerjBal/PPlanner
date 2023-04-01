using System;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SerjBal
{
    public static class Extensions
    {
        public static void Clear(this Transform transform)
        {
            foreach (Transform child in transform)
                Object.Destroy(child.gameObject);
        }

        public static string GetHexColor(this Color color)
        {
            var r = Mathf.RoundToInt(color.r * 255.0f);
            var g = Mathf.RoundToInt(color.g * 255.0f);
            var b = Mathf.RoundToInt(color.b * 255.0f);
            return $"#{r:X2}{g:X2}{b:X2}";
        }

        public static string ToPath(this DateTime dateTime)
        {
            return Path.Combine(Const.DataPath, dateTime.ToString("yyyy'-'MM'-'dd"));
        }
    }
}