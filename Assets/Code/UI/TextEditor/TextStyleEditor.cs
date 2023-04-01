using TMPro;
using UnityEngine;

namespace SerjBal
{
    public class TextStyleEditor
    {
        private const string _fontTag = "font";
        private const string _linkTag = "link";
        private const string _strikethroughTag = "s";
        private const string _underlineTag = "u";
        private const string _boldTag = "b";
        private const string _italicTag = "i";
        private const string _colorTag = "color";
        private readonly TMP_InputField _inputField;

        public TextStyleEditor(TMP_InputField inputField)
        {
            _inputField = inputField;
        }

        public void ApplyBoldStyle()
        {
            SetSelectedTextStyle(_boldTag);
        }

        public void ApplyItalicStyle()
        {
            SetSelectedTextStyle(_italicTag);
        }

        public void ApplyUnderline()
        {
            SetSelectedTextStyle(_underlineTag);
        }

        public void ApplyStrikethrough()
        {
            SetSelectedTextStyle(_strikethroughTag);
        }

        public void ApplyColor(Color col)
        {
            SetSelectedTextStyle($"{_colorTag}={col.GetHexColor()}", _colorTag);
        }

        public void ApplyLink(string link)
        {
            SetSelectedTextStyle($"{_linkTag}={link}", _linkTag);
        }

        public void ApplyFont(string font)
        {
            SetSelectedTextStyle($"{_fontTag}={font}", _fontTag);
        }

        private void SetSelectedTextStyle(string openTagLetter, string closeTagLetter = null)
        {
            var start = Mathf.Min(_inputField.selectionStringAnchorPosition, _inputField.selectionStringFocusPosition);
            var length =
                Mathf.Abs(_inputField.selectionStringAnchorPosition - _inputField.selectionStringFocusPosition);
            var selectedText = _inputField.text.Substring(start, length);

            var openTag = $"<{openTagLetter}>";
            var closeTag = closeTagLetter == null ? $"</{openTagLetter}>" : $"</{closeTagLetter}>";

            //Set selected as style

            // Replace the selected text in the input field with the transformed text
            var result = _inputField.text.Substring(0, start) + selectedText +
                         _inputField.text.Substring(start + length);

            var pattern = $"{openTag}{closeTag}";
            _inputField.text = result.Replace(pattern, "");
        }
    }
}