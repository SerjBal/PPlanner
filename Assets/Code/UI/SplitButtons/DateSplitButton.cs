using static System.IO.Path;

namespace SerjBal
{
    public class DateSplitButton : SplitButtonPresenter, IHierarchical
    {
        public override void Initialize(ButtonView view, Services services)
        {
            ItemType = MenuItemType.Date;
            InitializeCommands(services);
            InitializeBaseView(view, GetName(Path));
        }

        private void InitializeCommands(Services services)
        {
            RemoveCommand = new ButtonRemoveCmd(this, services);
            EditCommand = new DateEditCmd(this, services);
            CollapseEndCommand = new ButtonCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd(this);
            ContentUpdateCommand = new DateUpdateCmd(this, services);
            AddNewContentCommand = new ButtonEditCmd<NewChannelWindow>(this, services);
        }

        private string GetName(string path)
        {
            var name = GetFileName(path);
            var split = name.Split('-');
            int monthNum = int.Parse(split[1]);
            var day = split[2];
            var month = Const.MonthEnglishNames[monthNum];
            return $"{day} {month}";
        }
    }
}