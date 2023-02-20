using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class TextEditorViewModel : MonoBehaviour
    {
        public RectTransform rectTransform;
        public TMP_Dropdown fontDropdown;
        public Button boldStyleButton;
        public Button italicStyleButton;
        public Button underlineStyleButton;
        public Button strikethroughStyleButton;
        public Button colorStyleButton;
        public Button linkStyleButton;
        public TMP_InputField inputField;
        private Color _textColor;
        public string Key { get; set; }
        public IMenuItem Parent { get; set; }

        private TextStyleEditor _textEditor;
        private float _timer;
        private ISaveLoad _saveLoad;
        private IGUIModelView _gui;
        private IWindowsFactory _windowsFactory;
        
        public void Initialize(string key)
        {
            Services services = new Services();
            _saveLoad = services.Single<ISaveLoad>();
            _gui = services.Single<IGUIModelView>();
            _windowsFactory = services.Single<IWindowsFactory>();
            _textEditor = new TextStyleEditor(inputField);
            
            SetKey(key);
            Resize();
            Bind();
        }

        private void Bind()
        {
            strikethroughStyleButton.onClick.AddListener(_textEditor.ApplyStrikethrough);
            underlineStyleButton.onClick.AddListener(_textEditor.ApplyUnderline);
            boldStyleButton.onClick.AddListener(_textEditor.ApplyBoldStyle);
            italicStyleButton.onClick.AddListener(_textEditor.ApplyItalicStyle);
            colorStyleButton.onClick.AddListener(() => _textEditor.ApplyColor(_textColor));
            inputField.onValueChanged.AddListener(OnChangedSave);
            linkStyleButton.onClick.AddListener(OpenLinkWindow);
            colorStyleButton.onClick.AddListener(OpenColorWindow);
        }
        
        private void SetKey(string key)
        {
            Key = key;
            inputField.text = _saveLoad.LoadText(Key).text;
        }

        private void OnChangedSave(string value)
        {
            StopAllCoroutines();
            StartCoroutine(DelayAndSave(value));
        }

        private IEnumerator DelayAndSave(string value)
        {
            _timer = 2f;
            while (_timer>0)
            {
                _timer -= Time.deltaTime;
                yield return null;
            }

            _timer = 0;
            _saveLoad.SaveText(Key, new TextData{ text = value});
        }
        
        public async void OpenLinkWindow() => await _windowsFactory.CreateTextLinkStyleWindow(_textEditor);
        public async void OpenColorWindow() => await _windowsFactory.CreateTextColorWindow(_textEditor);
        public void Resize() => rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _gui.GetMenuBounds());
        private void OnDestroy() => _saveLoad.SaveText(Key, new TextData{ text = inputField.text});
    }
}