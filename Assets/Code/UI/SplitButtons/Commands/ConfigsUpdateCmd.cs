using System.Collections.Generic;
using System.IO;

namespace SerjBal
{
    internal class ConfigsUpdateCmd : ICommand
    {
        private readonly IMenuFactory _factory;
        private readonly IHierarchical _item;
        private readonly SplitButtonPresenter _presenter;

        public ConfigsUpdateCmd(IHierarchical item, Services services)
        {
            _presenter = item as SplitButtonPresenter;
            _item = item;
            _factory = services.Single<IMenuFactory>();
        }

        public async void Execute(object param = null)
        {
            _item.ContentContainer.Clear();
            _item.ChildList = new List<IHierarchical>();

            string path = Path.Combine(Const.DataPath, Const.ColorsSettingsFile);
            _item.ChildList.Add( await _factory.CreateButton<ColorsSettingsButton>(Const.ColorSettingsButtonPath, _item, path));

            _presenter.ExpandEndCommand?.Execute();
        }
    }
}