using SerjBal.Windows;
using TMPro;
using UnityEngine;

namespace SerjBal
{
    public class TimeWindowView : WindowView
    {
        [SerializeField] private TMP_Dropdown typeDropdown;
        private EditTimeWindow _viewModel;

        public override void Initialize(IWindowViewModel viewModel)
        {
            base.Initialize(viewModel);
            _viewModel = viewModel as EditTimeWindow;
            typeDropdown.onValueChanged.AddListener(delegate { SetPost(typeDropdown.value); });
        }

        private void SetPost(int value) => _viewModel.TypeOfPost = (PostType)value;
    }
}