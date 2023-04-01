using System.Collections.Generic;
using UnityEngine;

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
            _viewModel.ContentContainer.gameObject.SetActive(false);
            ((Canvas)param)!.overrideSorting = false;
            foreach (Transform item in _viewModel.ContentContainer) Object.Destroy(item.gameObject);
        }
    }
}