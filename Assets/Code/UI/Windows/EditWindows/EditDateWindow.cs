using System.IO;

namespace SerjBal
{
    public class EditDateWindow : EditWindow
    {
        public override void Initialize(IHierarchical splitButton, Services services)
        {
            CheckCmd = new ButtonCheckCmd(this, splitButton.Path);
            СonfirmCmd = new WarningWindowCreateCmd<ConfirmWindow>(this, services);
            AcceptCmd = new DateOverrideCmd(this, services, splitButton);
            FormatCmd = new DataFormatCmd(this, services);

            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.EditDateWindowFormatText;
            AcceptButtonText = Const.EditDateWindowButtonText;
            InputString = Path.GetFileName(splitButton.Path);
        }
    }
}