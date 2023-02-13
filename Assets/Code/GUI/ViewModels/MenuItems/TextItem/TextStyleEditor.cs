using TMPro;
using UnityEngine;

namespace SerjBal
{
    public class TextStyleEditor
    {
        private TMP_InputField _inputField;
        private int start, length;
        private string selectedText;
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
            start = Mathf.Min(_inputField.selectionStringAnchorPosition, _inputField.selectionStringFocusPosition);
            length = Mathf.Abs(_inputField.selectionStringAnchorPosition - _inputField.selectionStringFocusPosition);
            selectedText = _inputField.text.Substring(start, length);
            
            string boldOpenTag = $"<{openTagLetter}>";
            string boldCloseTag = closeTagLetter==null? $"</{openTagLetter}>":$"</{closeTagLetter}>";

            int boldOpenTagIndex = selectedText.IndexOf(boldOpenTag);
            int boldCloseTagIndex = selectedText.IndexOf(boldCloseTag);

            if (boldOpenTagIndex != -1 && boldCloseTagIndex != -1)
            {
                // Remove existing bold tags
                selectedText = selectedText.Remove(boldCloseTagIndex, boldCloseTag.Length);
                selectedText = selectedText.Remove(boldOpenTagIndex, boldOpenTag.Length);
            }
            else
            {
                // Add bold tags
                selectedText = boldOpenTag + selectedText + boldCloseTag;
            }

            ReplaceSelectedText();
        }
        public void ApplyBoldStyle() => SetSelectedTextStyle(_boldTag);
        public void ApplyItalicStyle() => SetSelectedTextStyle(_italicTag);
        public void ApplyUnderline() => SetSelectedTextStyle(_underlineTag);
        public void ApplyStrikethrough() => SetSelectedTextStyle(_strikethroughTag);
        public void ApplyColor(Color col) => SetSelectedTextStyle($"{_colorTag}={col.GetHexColor()}", _colorTag);
        public void ApplyLink(string link) => SetSelectedTextStyle($"{_linkTag}={link}", _linkTag);
        public void ApplyFont(string font) => SetSelectedTextStyle($"{_fontTag}={font}", _fontTag);


        private void ReplaceSelectedText()
        {
            string beforeSelection = _inputField.text.Substring(0, start);
            string afterSelection = _inputField.text.Substring(start + length);
            _inputField.text = beforeSelection + selectedText + afterSelection;
        }
    }
}