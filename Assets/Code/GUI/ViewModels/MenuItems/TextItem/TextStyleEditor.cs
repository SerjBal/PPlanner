using System;
using System.Linq;
using System.Text.RegularExpressions;
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
        
        private void SetSelectedTextStyle(string openTagLetter, string closeTagLetter = null)
        {
            int start = Mathf.Min(_inputField.selectionStringAnchorPosition, _inputField.selectionStringFocusPosition);
            int length = Mathf.Abs(_inputField.selectionStringAnchorPosition - _inputField.selectionStringFocusPosition);
            string selectedText = _inputField.text.Substring(start, length);

            string openTag = $"<{openTagLetter}>";
            string closeTag = closeTagLetter==null?$"</{openTagLetter}>":$"</{closeTagLetter}>";

            bool isSelectedContainsOpenTag = selectedText.Contains(openTag);
            bool isSelectedContainsCloseTag = selectedText.Contains(closeTag);
            bool isSelectedTextBold = isSelectedContainsOpenTag && isSelectedContainsCloseTag;
            bool isTextAroundSelectionBold = _inputField.text.Substring(0, start).Contains(openTag) && _inputField.text.Substring(start + length).Contains(closeTag);

            if (isSelectedTextBold)
                selectedText = selectedText.Replace(openTag, "").Replace(closeTag, "");
            else if (isSelectedContainsOpenTag)
                selectedText = $"{openTag}{selectedText.Replace(openTag, "")}";
            else if (isSelectedContainsCloseTag)
                selectedText = $"{selectedText.Replace(closeTag, "")}{closeTag}";
            if (isTextAroundSelectionBold)
                selectedText = $"{closeTag}{selectedText}{openTag}";
            else 
                selectedText = $"{openTag}{selectedText}{closeTag}";

            // Replace the selected text in the input field with the transformed text
            var result = _inputField.text.Substring(0, start) + selectedText + _inputField.text.Substring(start + length);

            string pattern = $"{openTag}{closeTag}";
            _inputField.text = result.Replace(pattern, "");
        }
        public void ApplyBoldStyle() => SetSelectedTextStyle(_boldTag);
        public void ApplyItalicStyle() => SetSelectedTextStyle(_italicTag);
        public void ApplyUnderline() => SetSelectedTextStyle(_underlineTag);
        public void ApplyStrikethrough() => SetSelectedTextStyle(_strikethroughTag);
        public void ApplyColor(Color col) => SetSelectedTextStyle($"{_colorTag}={col.GetHexColor()}", _colorTag);
        public void ApplyLink(string link) => SetSelectedTextStyle($"{_linkTag}={link}", _linkTag);
        public void ApplyFont(string font) => SetSelectedTextStyle($"{_fontTag}={font}", _fontTag);
    }
}