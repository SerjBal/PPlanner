using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class ButtonCollapseEndCmd : ICommand
    {
        private readonly SplitButtonPresenter _presenter;

        public ButtonCollapseEndCmd(SplitButtonPresenter presenter)
        {
            _presenter = presenter;
        }

        public void Execute(object param = null)
        {
            ContentClear();
            LayoutUpdate(param);
        }

        private void LayoutUpdate(object param)
        {
            ((Canvas)param)!.overrideSorting = false;
            var layout = _presenter.ContentContainer.GetComponent<VerticalLayoutGroup>();
            if (layout) layout.enabled = true;
        }

        private void ContentClear()
        {
            _presenter.ChildList = new List<IHierarchical>();
            _presenter.ContentContainer.Clear();
        }
    }
}