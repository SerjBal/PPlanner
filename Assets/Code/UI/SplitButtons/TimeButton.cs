using System.IO;
using SerjBal.Indication;

namespace SerjBal
{
    public class TimeButton : ButtonViewModel, IHierarchical
    {
        public override void Initialize(Services services)
        {
            ItemType = MenuItemType.Time;

            RemoveCommand = new ButtonRemoveCmd(this, services);
            EditCommand = new TimeEditCmd<EditTimeWindow>(this, services);
            CollapseEndCommand = new ButtonCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd( this);
            ContentUpdateCommand = new TextEditorAddCmd(this, services);
            AddNewContentCommand = default;
        }
    }
}