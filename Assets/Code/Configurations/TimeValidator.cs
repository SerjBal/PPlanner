using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SerjBal
{
    [CreateAssetMenu(fileName = "Input Field Validator", menuName = "Input Field Validator")]
    public class TimeValidator : TMPro.TMP_InputValidator
    {
        private const string TimePattern = "^([0-1][0-9]|2[0-3]):([0-5][0-9])$"; 
        private readonly string _defaultText = "00:00";
        
        public override char Validate(ref string text, ref int pos, char ch)
        {
            text = OnValueChanged(text);
            if (ch == '\b')
            {
                pos = 0;
                return '0';
            }
           
            if (!char.IsDigit(ch) || text.Length > 5)
                return '\0';
            
            if (pos < 5) 
                text = text.Remove(pos, 1).Insert(pos, ch.ToString());

            if (pos == 1)
                pos += 2;
            else
                pos++;

            return ch;
        }
        
        private string OnValueChanged(string text)
        {
            if (!Regex.IsMatch(text, TimePattern))
                return _defaultText;

            return text;
        }
    }
}