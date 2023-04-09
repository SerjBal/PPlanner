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
        
        public static int ToMinutes(this string time)
        {
            var split = time.Split(':');
            return int.Parse(split[0]) * 60 + int.Parse(split[1]);
        }
        
        public static void CommandExecute(this ButtonViewModel button, CommandType commandType, Object param = null)
        {
            switch (commandType)
            {
                case CommandType.Remove:
                    button.RemoveCommand?.Execute(param);
                    break;
                case CommandType.Edit:
                    button.EditCommand?.Execute(param);
                    break;
                case CommandType.UpdateContent:
                    button.ContentUpdateCommand?.Execute(param);
                    break;
                case CommandType.AddNewContent:
                    button.AddNewContentCommand?.Execute(param);
                    break;
                case CommandType.CollapseEnd:
                    button.CollapseEndCommand?.Execute(param);
                    break;
                case CommandType.CollapseStart:
                    button.CollapseStartCommand?.Execute(param);
                    break;
                case CommandType.ExpandEnd:
                    button.ExpandEndCommand?.Execute(param);
                    break;
                case CommandType.ExpandStart:
                    button.ExpandStartCommand?.Execute(param);
                    break;
                default:
                    Debug.LogError("Unknown command");
                    break;
            }
        }
    }
    public enum CommandType
    {
        Remove, Edit, UpdateContent, AddNewContent, CollapseEnd, CollapseStart, ExpandEnd, ExpandStart
    }
}