using System;
using System.Collections;
using UnityEngine;

namespace SerjBal
{
    public class ExpandAnimator : MonoBehaviour
    {
        public Action onExpandEvent;
        public Action onCollapsedEvent;
        private AnimationCurve _expandAnimationCurve;
        private float _buttonHeight;
        [SerializeField]private RectTransform buttonTransform;
        private RectTransform _parentTransform;

        public void Initialize(AnimationCurve expandAnimationCurve)
        {
            _expandAnimationCurve = expandAnimationCurve;
            _parentTransform = buttonTransform.parent.GetComponent<RectTransform>();
            _buttonHeight = buttonTransform.rect.height;
        }
        public void PlayClose()
        {
            StartCoroutine(Collapse());
        }

        public void PlayOpen()
        {
            StartCoroutine(Expand());
        }

        private IEnumerator Expand()
        {
            buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _parentTransform.rect.height);
            onExpandEvent.Invoke();
            yield break;
        }
        
        private IEnumerator Collapse()
        {
            buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _buttonHeight);
            onCollapsedEvent.Invoke();
            yield break;
        }
    }
}