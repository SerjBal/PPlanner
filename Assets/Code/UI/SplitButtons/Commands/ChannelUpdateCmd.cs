using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        
        private new async Task AddContent()
        {
            var content = _data.LoadDirectory(item.ContentPath);
            Array.Sort(content, new TimeStringComparer());

            foreach (string button in content) 
                item.ChildList.Add(await factory.CreateMenuButton(item, button));
        }
        
        class TimeStringComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                x = Path.GetFileName(x);
                y = Path.GetFileName(y);
                TimeSpan timeX = TimeSpan.Parse(x);
                TimeSpan timeY = TimeSpan.Parse(y);

                return timeX.CompareTo(timeY);
            }
        }
    }
}