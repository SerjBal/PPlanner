using System.IO;

namespace SerjBal
{
    public class LoadTemplateCmd : ICommand
    {
        private readonly IHierarchical _viewModel;
        private readonly ITemplatesProvider _templates;
        private readonly IDataProvider _data;

        public LoadTemplateCmd(IHierarchical viewModel, Services services)
        {
            _viewModel = viewModel;
            _templates = services.Single<ITemplatesProvider>();
            _data = services.Single<IDataProvider>();
        }

        public void Execute(object param = null)
        {
            var templatePath = _viewModel.Path;
            _templates.Load(templatePath);
        }
    }
}