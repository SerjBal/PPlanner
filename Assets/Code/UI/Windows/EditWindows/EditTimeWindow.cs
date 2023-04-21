using System.IO;
using SerjBal.Windows;

namespace SerjBal
{
    public class EditTimeWindow : EditWindow
    {
        public override void Initialize(IHierarchical splitButton, Services services, WindowView view)
        {
            CheckCmd = new ItemCheckNameCmd(this, splitButton.Parent.ContentPath);
            СonfirmCmd = new WarningWindowCreateCmd<ConfirmWindow>(this, services);
            AcceptCmd = new TimeOverrideCmd(this, services, splitButton);
            FormatCmd = new TimeFormatCmd(this, view.inputField);

            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.EditTimeWindowFormatText;
            AcceptButtonText = Const.EditWindowButtonText;
            
            InputString = Path.GetFileName(splitButton.Path);
           
            InitializeView(view);
            InitializeTimeEditView(view);
        }

        private void InitializeTimeEditView(WindowView view)
        {
            var timeView = (TimeWindowView)view;
            timeView.typeDropdown.onValueChanged.AddListener(delegate { TypeOfPost = (PostType)timeView.typeDropdown.value; });
        }

        public PostType TypeOfPost { get; set; }
    }
}