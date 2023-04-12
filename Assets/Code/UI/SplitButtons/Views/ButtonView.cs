using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class ButtonView : MonoBehaviour, IView
    {
        [SerializeField] protected Canvas canvas;
        [SerializeField] protected SwipeController controller;
        [SerializeField] protected Button editButton;
        [SerializeField] protected Button removeButton;
        [SerializeField] protected TMP_Text nameText;
        [SerializeField] protected Transform contentContainer;
        [SerializeField] protected ButtonAnimator animator;

        public void Initialize(ButtonConfig config)
        {
            if (animator) animator.Initialize(config.expandAnimationCurve);
            if (controller) controller.Initialize(config);
            contentContainer.gameObject.SetActive(false);
        }

        public void ReleaseSetup(ButtonViewModel viewModel)
        {
            nameText.text = GetName(viewModel.Path);
            canvas.overrideSorting = viewModel.IsOverrideSorting;
            
            viewModel.ContentContainer = contentContainer;
            viewModel.OnKeyChanged = GetPath;
            viewModel.OnOverrideSortingChanged = OverrideSorting;
            viewModel.OnExpandStateChanged += Animate;

            if (animator)
            {
                animator.onExpandStartEvent = () => viewModel.CommandExecute(CommandType.ExpandStart, canvas);
                animator.onExpandEndEvent = () => viewModel.CommandExecute(CommandType.ExpandEnd);
                animator.onCollapseStartEvent = () => viewModel.CommandExecute(CommandType.CollapseStart);
                animator.onCollapseEndEvent = () => viewModel.CommandExecute(CommandType.CollapseEnd, canvas);
            }
            if (controller)
            {
                editButton.onClick.AddListener(() => viewModel.CommandExecute(CommandType.Edit));
                removeButton.onClick.AddListener(() => viewModel.CommandExecute(CommandType.Remove));
                controller.onSelectedEvent = viewModel.PushButton;
            }
        }

        private protected virtual string GetName(string path) => nameText.text = Path.GetFileName(path);
        private void OverrideSorting(bool value) => canvas.overrideSorting = value;
        private void GetPath(string path) => nameText.text = Path.GetFileName(path);

        private void Animate(bool value)
        {
            if (value)
                animator?.PlayOpen();
            else
                animator?.PlayClose();
        }
    }
}