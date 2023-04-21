using System.IO;
using SerjBal.Windows;

namespace SerjBal
{
    public class EditChannelWindow : EditWindow
    {
        public override void Initialize(IHierarchical splitButton, Services services,  WindowView view)
        {
            CheckCmd = new ItemCheckNameCmd(this, splitButton.Parent.ContentPath);
            СonfirmCmd = new WarningWindowCreateCmd<ConfirmWindow>(this, services);
            AcceptCmd = new ButtonOverrideCmd(this, services, splitButton);
            FormatCmd = new NameFormatCmd(this);

            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.EditChannelWindowFormatText;
            AcceptButtonText = Const.EditWindowButtonText;
            
            InputString = Path.GetFileName(splitButton.Path);
            InitializeView(view);
        }
    }
}