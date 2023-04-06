using System.Collections.Generic;
using System.Threading.Tasks;

namespace SerjBal
{
    public class ButtonUpdateCmd : ICommand
    {
        private protected readonly IMenuFactory factory;
        private protected readonly IHierarchical item;
        private protected readonly IDataProvider data;
        private readonly ButtonExpandEndCmd _buttonExpandEndCmd;
        private readonly ButtonViewModel _viewModel;

        public ButtonUpdateCmd(IHierarchical item, Services services)
        {
            _buttonExpandEndCmd = new ButtonExpandEndCmd();
            _viewModel = item as ButtonViewModel;
            this.item = item;
            factory = services.Single<IMenuFactory>();
            data = services.Single<IDataProvider>();
        }

        public virtual async void Execute(object param = null)
        {
            ClearContent();
            await AddContent();
            await AddNewItemButton();
            EndCommand();
        }

        private protected void EndCommand() => _buttonExpandEndCmd.Execute();

        private protected async Task AddNewItemButton()
        {
            var addButton = await factory.CreateAddButton(item.ContentContainer);
            addButton.onClick.AddListener(() => _viewModel.AddNewContentCommand.Execute());
        }

        private protected async Task AddContent()
        {
            var content = data.LoadDirectory(item.ContentPath);
            for (var i = 0; i < content.Length; i++)
                item.ChildList.Add(await factory.CreateButton(item, content[i]));
        }

        private protected void ClearContent()
        {
            item.ContentContainer.Clear();
            item.ChildList = new List<IHierarchical>();
        }
    }
}