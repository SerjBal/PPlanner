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
            ContentClear();
            LayoutUpdate(param);
        }

        private void LayoutUpdate(object param)
        {
            ((Canvas)param)!.overrideSorting = false;
            var layout = _viewModel.ContentContainer.GetComponent<VerticalLayoutGroup>();
            if (layout) layout.enabled = true;
        }

        private void ContentClear()
        {
            _viewModel.ChildList = new List<IHierarchical>();
            _viewModel.ContentContainer.Clear();
        }
    }
}