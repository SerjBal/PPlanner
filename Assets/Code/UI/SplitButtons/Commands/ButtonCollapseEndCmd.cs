using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class ButtonCollapseEndCmd : ICommand
    {
        private readonly ButtonViewModel _viewModel;

        public ButtonCollapseEndCmd(ButtonViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Execute(object param = null)
        {
            _viewModel.ChildList = new List<IHierarchical>();
            _viewModel.ContentContainer.Clear();
            ((Canvas)param)!.overrideSorting = false;
            foreach (Transform item in _viewModel.ContentContainer) Object.Destroy(item.gameObject);
            var layout = _viewModel.ContentContainer.GetComponent<VerticalLayoutGroup>();
            if (layout) layout.enabled = true;
        }
    }
}