using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class TextEditorViewModel : MonoBehaviour
    {
        [SerializeField] private GameObject toolScreen;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private TMP_Dropdown fontDropdown;
        [SerializeField] private Button boldStyleButton;
        [SerializeField] private Button italicStyleButton;
        [SerializeField] private Button underlineStyleButton;
        [SerializeField] private Button strikethroughStyleButton;
        [SerializeField] private Button colorStyleButton;
        [SerializeField] private Button linkStyleButton;
        [SerializeField] private TMP_InputField inputField;
        private IDataProvider _data;
        private Color _textColor;

        private TextStyleEditor _textEditor;
        private float _timer;
        public string path { get; set; }
        public IHierarchical Parent { get; set; }

        private void OnDestroy()
        {
            _data.CreateFile(path, new PostData { text = inputField.text });
        }

        public void Initialize(string path)
        {
            var services = new Services();
            _data = services.Single<IDataProvider>();
            _textEditor = new TextStyleEditor(inputField);

            SetPath(path);
            Bind();
        }

        public void SetReadOnly(bool isTrue)
        {
            inputField.readOnly = isTrue;
            toolScreen.SetActive(!isTrue);
        }

        private void Bind()
        {
            strikethroughStyleButton.onClick.AddListener(_textEditor.ApplyStrikethrough);
            underlineStyleButton.onClick.AddListener(_textEditor.ApplyUnderline);
            boldStyleButton.onClick.AddListener(_textEditor.ApplyBoldStyle);
            italicStyleButton.onClick.AddListener(_textEditor.ApplyItalicStyle);
            colorStyleButton.onClick.AddListener(() => _textEditor.ApplyColor(_textColor));
            inputField.onValueChanged.AddListener(OnChangedSave);
        }

        private void SetPath(string path)
        {
            this.path = Path.Combine(path, $"{Const.TextItemName}.json");
            inputField.text = _data.LoadFile<PostData>(this.path).text;
        }

        private void OnChangedSave(string value)
        {
            StopAllCoroutines();
            StartCoroutine(DelayAndSave(value));
        }

        private IEnumerator DelayAndSave(string value)
        {
            _timer = 1f;
            while (_timer > 0)
            {
                _timer -= Time.deltaTime;
                yield return null;
            }

            _timer = 0;
            _data.CreateFile(path, new PostData { text = value });
        }
    }
}