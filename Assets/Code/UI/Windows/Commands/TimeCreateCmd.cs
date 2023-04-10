using System.IO;
using SerjBal.Indication;

namespace SerjBal
{
    public class TimeCreateCmd : ButtonCreateCmd
    {
        private readonly IDataProvider _dataProvider;
        private readonly IHierarchical _itemViewModel;
        private readonly IWindowViewModel _viewModel;
        private readonly IPostIndication _indication;

        public TimeCreateCmd(IWindowViewModel viewModel, Services services, IHierarchical itemViewModel)
            : base(viewModel, services, itemViewModel)
        {
            _indication = services.Single<IPostIndication>();
        }

        public override void Execute(object param = null)
        {
            SavePostType((string)param);
            base.Execute(param);
        }
        private void SavePostType(string newItem)
        {
            var newPath = Path.Combine(itemViewModel.ContentPath, newItem);
            var postType = (viewModel as EditTimeWindow)?.TypeOfPost;
            _indication.SavePostType(newPath, postType ?? PostType.Content);
        }
    }
}