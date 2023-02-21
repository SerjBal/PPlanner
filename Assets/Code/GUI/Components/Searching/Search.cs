using System;
using UnityEngine;

namespace SerjBal
{
    public class Search
    {
        public int FindText(string text, string textToFind)
        {
            bool ignoreCaseSearchResult = text.StartsWith("extension", System.StringComparison.CurrentCultureIgnoreCase);
            var startResult = ($"Starts with \"extension\"? {ignoreCaseSearchResult} (ignoring case)");

            bool endsWithSearchResult = text.EndsWith(".", System.StringComparison.CurrentCultureIgnoreCase);
            var endResult = ($"Ends with '.'? {endsWithSearchResult}");

            return -1;
        }
    }
}