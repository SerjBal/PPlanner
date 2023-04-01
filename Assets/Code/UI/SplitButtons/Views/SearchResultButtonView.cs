using System.IO;
using TMPro;
using UnityEngine;

namespace SerjBal
{
    public class SearchResultButtonView : ButtonView
    {
        [SerializeField] private TextMeshProUGUI text;

        public override void Setup(ButtonViewModel viewModel)
        {
            base.Setup(viewModel);
            text.text = Path.GetFileName(viewModel.Path);
        }
    }
}