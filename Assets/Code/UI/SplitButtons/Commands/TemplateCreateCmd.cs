using System.IO;

namespace SerjBal
{
    public class TemplateCreateCmd : ICommand
    {
        private readonly IWindowPresenter _newTemplateWindow;
        private readonly IHierarchical _itemViewModel;
        private readonly ITemplatesProvider _templates;
        private readonly IDataProvider _data;
        private readonly object _viewModel;

        public TemplateCreateCmd(IWindowPresenter newTemplateWindow, Services services, IHierarchical itemViewModel)
        {
            _newTemplateWindow = newTemplateWindow;
            _itemViewModel = itemViewModel;
            _templates = services.Single<ITemplatesProvider>();
            _data = services.Single<IDataProvider>();
        }

        public void Execute(object param = null)
        {
            _templates.Create(_newTemplateWindow.InputString);
            (_itemViewModel as SplitButtonPresenter)?.ContentUpdateCommand?.Execute();
            _newTemplateWindow.OnClose.Invoke();
        }
    }
}