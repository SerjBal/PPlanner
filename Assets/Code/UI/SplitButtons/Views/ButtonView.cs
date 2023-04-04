using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class ButtonView : MonoBehaviour
    {
        [SerializeField] protected Canvas canvas;
        [SerializeField] protected SwipeController controller;
        [SerializeField] protected Button editButton;
        [SerializeField] protected Button removeButton;
        [SerializeField] protected TMP_Text nameText;
        [SerializeField] protected Transform contentContainer;
        [SerializeField] protected ButtonAnimator animator;

        public void Initialize(ButtonConfigs configs)
        {
            if (animator) animator.Initialize(configs.expandAnimationCurve);
            if (controller) controller.Initialize(configs);
            contentContainer.gameObject.SetActive(false);
        }

        public virtual void Setup(ButtonViewModel viewModel)
        {
            nameText.text = Path.GetFileName(viewModel.Path);
            canvas.overrideSorting = viewModel.IsCanvasActive;
            
            viewModel.ContentContainer = contentContainer;
            viewModel.OnKeyChanged += path => nameText.text = Path.GetFileName(path);
            viewModel.OnCanvasChanged += value => canvas.overrideSorting = value;
            viewModel.OnExpandStateChanged += value =>
            {
                if (value)
                    animator.PlayOpen();
                else
                    animator.PlayClose();
            };
            
            animator.onExpandStartEvent = () => viewModel.ExpandStartCommand?.Execute(canvas);
            animator.onExpandFinishEvent = () => viewModel.ExpandEndCommand?.Execute();
            animator.onCollapseStartEvent = () => viewModel.CollapseStartCommand?.Execute();
            animator.onCollapseFinishEvent = () => viewModel.CollapseFinishEnd?.Execute(canvas);
            if (controller)
            {
                editButton.onClick.AddListener(() => viewModel.EditCommand.Execute());
                removeButton.onClick.AddListener(() => viewModel.RemoveCommand.Execute());
                controller.onSelectedEvent = viewModel.PushButton;
            }
        }
    }
}