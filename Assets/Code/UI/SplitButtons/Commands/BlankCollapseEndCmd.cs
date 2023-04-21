using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class BlankCollapseEndCmd : ICommand
    {
        private readonly SplitButtonPresenter _presenter;

        public BlankCollapseEndCmd(SplitButtonPresenter presenter) => _presenter = presenter;

        public void Execute(object param = null) => LayoutUpdate(param);

        private void LayoutUpdate(object param)
        {
            ((Canvas)param)!.overrideSorting = false;
            var layout = _presenter.ContentContainer.GetComponent<VerticalLayoutGroup>();
            if (layout) layout.enabled = true;
        }
    }
}