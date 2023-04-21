using SerjBal.Indication;

namespace SerjBal
{
    public class TimeOverrideCmd : ButtonOverrideCmd
    {
        private readonly IPostIndicator _indicator;

        public TimeOverrideCmd(IWindowPresenter presenter, Services services, IHierarchical itemViewModel)
            : base(presenter, services, itemViewModel) => _indicator = services.Single<IPostIndicator>();

        public override void Execute(object param = null)
        {
            SavePostType();
            base.Execute();
        }

        private void SavePostType()
        {
            var path = GetNewPath();
            var postType = (presenter as EditTimeWindow)?.TypeOfPost;
            _indicator.SavePostType(path, postType ?? PostType.Content);
        }
    }
}