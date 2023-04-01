using System.Collections.Generic;

namespace SerjBal
{
    public class ButtonUpdateCmd : ICommand
    {
        private readonly IDataProvider _data;
        private readonly IMenuFactory _factory;
        private readonly IHierarchical _item;
        private readonly ButtonExpandEndCmd _buttonExpandEndCmd;
        private readonly ButtonViewModel _viewModel;

        public ButtonUpdateCmd(IHierarchical item, Services services)
        {
            _buttonExpandEndCmd = new ButtonExpandEndCmd();
            _viewModel = item as ButtonViewModel;
            _item = item;
            _factory = services.Single<IMenuFactory>();
            _data = services.Single<IDataProvider>();
        }

        public async void Execute(object param = null)
        {
            _item.ContentContainer.Clear();
            _item.ChildList = new List<IHierarchical>();

            var content = _data.LoadDirectory(_item.Path);
            for (var i = 0; i < content.Length; i++)
                _item.ChildList.Add(await _factory.CreateButton(_item, content[i]));

            var addButton = await _factory.CreateAddButtonItem(_item.ContentContainer);
            addButton.onClick.AddListener(() => _viewModel.AddNewContentCommand.Execute());
            _buttonExpandEndCmd.Execute();
        }
    }
}