using TMPro;
using UnityEngine;

namespace SerjBal
{
    public class TextStyleEditor
    {
        private TMP_InputField _inputField;
        private const string _fontTag = "font";
        private const string _linkTag = "link";
        private const string _strikethroughTag = "s";
        private const string _underlineTag = "u";
        private const string _boldTag = "b";
        private const string _italicTag = "i";
        private const string _colorTag = "color";
        public TextStyleEditor(TMP_InputField inputField) => _inputField = inputField;

        public void ApplyBoldStyle() => SetSelectedTextStyle(_boldTag);                                            
        public void ApplyItalicStyle() => SetSelectedTextStyle(_italicTag);                                        
        public void ApplyUnderline() => SetSelectedTextStyle(_underlineTag);                                       
        public void ApplyStrikethrough() => SetSelectedTextStyle(_strikethroughTag);                               
        public void ApplyColor(Color col) => SetSelectedTextStyle($"{_colorTag}={col.GetHexColor()}", _colorTag);  
        public void ApplyLink(string link) => SetSelectedTextStyle($"{_linkTag}={link}", _linkTag);                
        public void ApplyFont(string font) => SetSelectedTextStyle($"{_fontTag}={font}", _fontTag);                
        
        private void SetSelectedTextStyle(string openTagLetter, string closeTagLetter = null)
        {
            int start = Mathf.Min(_inputField.selectionStringAnchorPosition, _inputField.selectionStringFocusPosition);
            int length = Mathf.Abs(_inputField.selectionStringAnchorPosition - _inputField.selectionStringFocusPosition);
            string selectedText = _inputField.text.Substring(start, length);

            string openTag = $"<{openTagLetter}>";
            string closeTag = closeTagLetter == null ? $"</{openTagLetter}>" : $"</{closeTagLetter}>";

            bool isContainsOpenTag = selectedText.Contains(openTag);
            bool isContainsCloseTag = selectedText.Contains(closeTag);
            bool isLeftSideContainsTag = CheckTagBalance(openTag, closeTag, _inputField.text, 0, start);                                    
            bool isRightSideContainsTag = CheckTagBalance(openTag, closeTag, _inputField.text, start + length, _inputField.text.Length);    
            bool isSelectedTextBold = isContainsOpenTag || isContainsCloseTag || isLeftSideContainsTag || isRightSideContainsTag;

            if (isSelectedTextBold)
                selectedText = selectedText.Replace(openTag, "").Replace(closeTag, "");
            else
                selectedText = $"{openTag}{selectedText}{closeTag}";

            // Replace the selected text in the input field with the transformed text
            var result = _inputField.text.Substring(0, start) + selectedText +
                         _inputField.text.Substring(start + length);

            string pattern = $"{openTag}{closeTag}";
            _inputField.text = result.Replace(pattern, "");
        }
        private bool CheckTagBalance(string openTag, string closeTag, string text, int startIndex, int endIndex)     
        {
            int openTagCount = 0;
            int closeTagCount = 0;

            for (int i = startIndex; i < endIndex; i++)
            {
                if (text.Substring(i, openTag.Length) == openTag)
                    openTagCount++;
                else if (text.Substring(i, closeTag.Length) == closeTag) closeTagCount++;
            }

            return openTagCount != closeTagCount;
        }
    }
}