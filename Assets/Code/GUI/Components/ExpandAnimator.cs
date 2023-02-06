using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class ExpandAnimator : MonoBehaviour
    {
        [SerializeField] VerticalLayoutGroup layoutGroup;
        public Action onExpandEvent;
        public Action onCollapsedEvent;
        private AnimationCurve _expandAnimationCurve;
        private bool _isExpaned;
        private float _buttonHeight;
        [SerializeField]private RectTransform buttonTransform;
        private IGUIModelView _gui;

        public void Initialize(AnimationCurve expandAnimationCurve)
        {
            _expandAnimationCurve = expandAnimationCurve;
            _gui = new Services().Single<IGUIModelView>();
            _buttonHeight = buttonTransform.rect.height;
        }
        public void AnimationPlay()
        {
            if (_isExpaned) PlayClose();
            else PlayOpen();
            _isExpaned = !_isExpaned;
        }
        private void PlayClose()
        {
            StartCoroutine(Collapse());
        }

        private void PlayOpen()
        {
            StartCoroutine(Expand());
        }
        private IEnumerator Expand()
        {
            buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _gui.GetMenuBounds());
            onExpandEvent.Invoke();
            layoutGroup.SetLayoutVertical();
            yield break;
        }
        
        private IEnumerator Collapse()
        {
            buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _buttonHeight);
            onCollapsedEvent.Invoke();
            layoutGroup.SetLayoutVertical();
            yield break;
        }
    }
}