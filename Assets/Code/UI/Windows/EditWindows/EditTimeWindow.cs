using System.IO;

namespace SerjBal
{
    public class EditTimeWindow : EditWindow
    {
        public override void Initialize(IHierarchical splitButton, Services services)
        {
            CheckCmd = new ButtonCheckCmd(this, splitButton.Parent.ContentPath);
            СonfirmCmd = new WarningWindowCreateCmd<ConfirmWindow>(this, services);
            AcceptCmd = new TimeOverrideCmd(this, services, splitButton);
            FormatCmd = new TimeFormatCmd(this);

            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.EditTimeWindowFormatText;
            AcceptButtonText = Const.EditTimeWindowButtonText;
            InputString = Path.GetFileName(splitButton.Path);
        }

        public PostType TypeOfPost { get; set; }
    }
}