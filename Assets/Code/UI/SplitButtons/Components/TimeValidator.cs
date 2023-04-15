using UnityEngine;

namespace SerjBal
{
    [CreateAssetMenu(fileName = "Input Field Validator", menuName = "Input Field Validator")]
    public class TimeValidator : TMPro.TMP_InputValidator
    {
        public override char Validate(ref string text, ref int pos, char ch)
        {
            if (char.IsNumber(ch) && text.Length < 14)
            {
                if (text.Length<2) text = $"{ch}0:00";
                
                if (pos == 2) pos++;
                
                if (text.Length > pos)
                    text = text.Remove(pos, 1).Insert(pos, ch.ToString());
                else
                    text = text.Insert(pos, ch.ToString());
                if (text.Length > 5) text = text.Substring(0, 5);
                
                pos++;
                return ch;
            }
                return '\0';
        }
    }
}