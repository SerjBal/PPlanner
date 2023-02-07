using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class TextEditorViewModel : MonoBehaviour
    {
        public TMP_Dropdown fontDropdown;
        public Button boldStyleButton;
        public Button italicStyleButton;
        public Button underlineStyleButton;
        public Button strikethroughStyleButton;
        public Button colorStyleButton;
        public Button linkStyleButton;
        public TMP_InputField inputField;
        public string Key { get; set; }
        
    }
}