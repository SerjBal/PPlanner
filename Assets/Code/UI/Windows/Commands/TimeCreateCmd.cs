using System.IO;
using SerjBal.Indication;

namespace SerjBal
{
    public class TimeCreateCmd : ButtonCreateCmd
    {
        private readonly IDataProvider _dataProvider;
        private readonly IHierarchical _itemViewModel;
        private readonly IWindowPresenter _presenter;
        private readonly IPostIndicator _indicator;

        public TimeCreateCmd(IWindowPresenter presenter, Services services, IHierarchical itemViewModel)
            : base(presenter, services, itemViewModel) =>
            _indicator = services.Single<IPostIndicator>();

        public override void Execute(object param = null)
        {
            SavePostType((string)param);
            base.Execute(param);
        }
        private void SavePostType(string newItem)
        {
            var newPath = Path.Combine(itemViewModel.ContentPath, newItem);
            var postType = (presenter as EditTimeWindow)?.TypeOfPost;
            _indicator.SavePostType(newPath, postType ?? PostType.Content);
        }
    }
}