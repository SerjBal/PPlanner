using System.IO;
using System.Threading.Tasks;

namespace SerjBal
{
    public class TemplatesButtonUpdateCmd : ButtonUpdateCmd
    {
        private string _templatesPath;

        public TemplatesButtonUpdateCmd(IHierarchical item, Services services) : base(item, services) => 
            _templatesPath = Path.Combine(Const.DataPath, Const.TemplatesDirectory, Const.ContentDirectory);

        public override async void Execute(object param = null)
        {
            ClearContent();
            await AddTemplateContent();
            await AddNewItemButton();
            EndCommand();
        }
        private async Task AddTemplateContent()
        {
            var content = _data.LoadDirectory(_templatesPath);
            for (var i = 0; i < content.Length; i++)
                item.ChildList.Add(await factory.CreateTemplateButton(item, content[i]));
        }
    }
}