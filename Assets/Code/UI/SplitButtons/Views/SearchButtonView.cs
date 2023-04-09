using TMPro;
using UnityEngine;

namespace SerjBal
{
    public class SearchButtonView : ButtonView
    {
        [SerializeField] private TMP_InputField inputField;
        public void Setup(SearchButton button)
        {
            base.ReleaseSetup(button);
            inputField.onValueChanged.AddListener(button.SearchInitialize);
        }
    }
}