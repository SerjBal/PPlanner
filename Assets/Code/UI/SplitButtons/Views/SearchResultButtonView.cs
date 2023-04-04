using System.IO;
using TMPro;
using UnityEngine;

namespace SerjBal
{
    public class SearchResultButtonView : ButtonView
    {
        public override void Setup(ButtonViewModel button)
        {
            nameText.text = Path.GetRelativePath(Const.DataPath, button.Path);
            canvas.overrideSorting = button.IsCanvasActive;
            
            button.ContentContainer = contentContainer;
            button.OnKeyChanged += path => nameText.text = Path.GetFileName(path);
            button.OnCanvasChanged += value => canvas.overrideSorting = value;
            button.OnExpandStateChanged += value =>
            {
                if (value)
                    animator.PlayOpen();
                else
                    animator.PlayClose();
            };
            
            animator.onExpandStartEvent = () => button.ExpandStartCommand?.Execute(canvas);
            animator.onExpandFinishEvent = () => button.ExpandEndCommand?.Execute();
            animator.onCollapseStartEvent = () => button.CollapseStartCommand?.Execute();
            animator.onCollapseFinishEvent = () => button.CollapseFinishEnd?.Execute(canvas);
            if (controller)
            {
                editButton.onClick.AddListener(() => button.EditCommand.Execute());
                removeButton.onClick.AddListener(() => button.RemoveCommand.Execute());
                controller.onSelectedEvent = button.PushButton;
            }
        }
    }
}