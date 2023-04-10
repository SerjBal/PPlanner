using SerjBal.Indication;

namespace SerjBal
{
    public class TimeOverrideCmd : ButtonOverrideCmd
    {
        private readonly IPostIndication _indication;

        public TimeOverrideCmd(IWindowViewModel viewModel, Services services, IHierarchical itemViewModel)
            : base(viewModel, services, itemViewModel) => _indication = services.Single<IPostIndication>();

        public override void Execute(object param = null)
        {
            SavePostType();
            base.Execute();
        }

        private void SavePostType()
        {
            var path = GetNewPath();
            var postType = (viewModel as EditTimeWindow)?.TypeOfPost;
            _indication.SavePostType(path, postType ?? PostType.Content);
        }
    }
}