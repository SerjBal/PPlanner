using System.IO;
using SerjBal.Indication;

namespace SerjBal
{
    public class TimeButton : ButtonViewModel, IHierarchical
    {
        private IPostIndication _indication;

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

            _indication = services.Single<IPostIndication>();
        }

        public void CreateMetaData() => _indication.CreateDefaultState(Path);
    }
}