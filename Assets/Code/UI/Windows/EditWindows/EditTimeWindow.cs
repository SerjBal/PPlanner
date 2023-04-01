using System.IO;

namespace SerjBal
{
    public class EditTimeWindow : EditWindow
    {
        public override void Initialize(IHierarchical splitButton, Services services)
        {
            CheckCmd = new ButtonCheckCmd(this, splitButton.Path);
            СonfirmCmd = new WarningWindowCreateCmd<ConfirmWindow>(this, services);
            AcceptCmd = new ButtonOverrideCmd(this, services, splitButton);
            FormatCmd = new TimeFormatCmd(this);

            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.EditTimeWindowFormatText;
            AcceptButtonText = Const.EditTimeWindowButtonText;
            InputString = Path.GetFileName(splitButton.Path);
        }
    }
}