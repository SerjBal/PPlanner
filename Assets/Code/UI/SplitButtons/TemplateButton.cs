using UnityEngine;

namespace SerjBal
{
    public class TemplateButton : ButtonViewModel, IHierarchical
    {
        public override void Initialize(Services services)
        {
            RemoveCommand = new ButtonRemoveCmd(this, services);
            EditCommand = new ButtonEditCmd<EditTemplateNameWindow>(this, services);
            CollapseEndCommand = default;
            CollapseStartCommand = default;
            ExpandEndCommand = default;
            ExpandStartCommand = 
            ContentUpdateCommand = new LoadTemplateCmd(this, services);
            AddNewContentCommand = default;
            OnExpandStateChanged += LoadTemplate;
        }

        private void LoadTemplate(bool isTrue) => 
            ContentUpdateCommand.Execute();
    }
}