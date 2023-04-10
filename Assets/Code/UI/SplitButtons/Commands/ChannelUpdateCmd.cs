using System.IO;
using System.Threading.Tasks;

namespace SerjBal
{
    public class ChannelUpdateCmd : ButtonUpdateCmd
    {
        public ChannelUpdateCmd(IHierarchical item, Services services) : base(item,  services) { }

        public override async void Execute(object param = null)
        {
            ClearContent();
            await AddCommentsButton();
            UpdatePostsWidget();
            await AddContent();
            await AddNewItemButton();
            EndCommand();
        }

        private async Task AddCommentsButton()
        {
            string path = Path.Combine(item.Path, Const.CommentsName);
            item.ChildList.Add(await factory.CreateButton<CommentButton>(Const.CommentsButtonPath, item, path));
        }

        private void UpdatePostsWidget() => (viewModel as ChannelButton)?.UpdateWidget();
    }
}