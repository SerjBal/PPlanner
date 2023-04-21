using System.IO;
using System.Threading.Tasks;

namespace SerjBal
{
    public class DateUpdateCmd : ButtonUpdateCmd
    {
        public DateUpdateCmd(IHierarchical item, Services services) : base(item,  services) { }

        public override async void Execute(object param = null)
        {
            ClearContent();
            await AddCommentsButton();
            await AddContent();
            await AddNewItemButton();
            EndCommand();
        }

        private async Task AddCommentsButton()
        {
            string path = Path.Combine(item.Path, Const.CommentsName);
            item.ChildList.Add(await factory.CreateButton<CommentSplitButton>(Const.CommentsButtonPath, item, path));   
        }
    }
}