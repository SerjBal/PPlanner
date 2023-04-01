using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class ButtonAnimator : MonoBehaviour
    {
        [SerializeField] private ScrollRect contentScrollRect;
        [SerializeField] private RectTransform buttonTransform;
        private float _buttonHeight;
        private AnimationCurve _expandAnimationCurve;
        private IGUI _iguiView;
        private bool _isExpaned;
        private VerticalLayoutGroup _layout;
        private float _sizeB;
        private float _yPos;
        public Action onCollapseFinishEvent;
        public Action onCollapseStartEvent;
        public Action onExpandFinishEvent;
        public Action onExpandStartEvent;

        public void Initialize(AnimationCurve expandAnimationCurve)
        {
            _layout = transform.parent.GetComponent<VerticalLayoutGroup>();
            _expandAnimationCurve = expandAnimationCurve;
            _iguiView = new Services().Single<IGUI>();
            _buttonHeight = buttonTransform.rect.height;
        }

        public void PlayClose()
        {
            StopAllCoroutines();
            StartCoroutine(Collapse());
        }

        public void PlayOpen()
        {
            StopAllCoroutines();
            StartCoroutine(Expand());
        }

        private IEnumerator Expand()
        {
            OnExpandStart();
            float timer = 0;
            var maxTime = _expandAnimationCurve[_expandAnimationCurve.length - 1].time;
            while (timer < maxTime)
            {
                var t = _expandAnimationCurve.Evaluate(timer);
                var x = buttonTransform.anchoredPosition.x;
                buttonTransform.anchoredPosition = new Vector2(x, Mathf.Lerp(_yPos, 0, t));
                buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                    Mathf.Lerp(_buttonHeight, _sizeB, t));
                timer += Time.deltaTime;
                yield return null;
            }

            OnExpandFinish();
        }

        private IEnumerator Collapse()
        {
            OnCollapseStart();
            float timer = 0;
            var maxTime = _expandAnimationCurve[_expandAnimationCurve.length - 1].time;
            while (timer < maxTime)
            {
                var t = _expandAnimationCurve.Evaluate(timer);
                var x = buttonTransform.anchoredPosition.x;
                buttonTransform.anchoredPosition = new Vector2(x, Mathf.Lerp(0, _yPos, t));
                buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                    Mathf.Lerp(_sizeB, _buttonHeight, t));
                timer += Time.deltaTime;
                yield return null;
            }

            OnCollapseFinish();
        }

        private void OnExpandStart()
        {
            _iguiView.InteractionEnable(true);
            contentScrollRect.vertical = true;
            _yPos = buttonTransform.anchoredPosition.y;
            var parentYPositionInWorldSpace = buttonTransform.parent.position.y;
            _sizeB = parentYPositionInWorldSpace;
            if (_layout) _layout.enabled = false;
            onExpandStartEvent?.Invoke();
        }

        private void OnExpandFinish()
        {
            buttonTransform.anchoredPosition = new Vector2(buttonTransform.anchoredPosition.x, 0);
            buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _sizeB);
            _iguiView.InteractionEnable(false);
            onExpandFinishEvent?.Invoke();
        }

        private void OnCollapseStart()
        {
            contentScrollRect.vertical = false;
            _iguiView.InteractionEnable(true);
            onCollapseStartEvent?.Invoke();
        }

        private void OnCollapseFinish()
        {
            buttonTransform.anchoredPosition = new Vector2(buttonTransform.anchoredPosition.x, _yPos);
            buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _buttonHeight);
            _iguiView.InteractionEnable(false);
            if (_layout) _layout.enabled = true;
            onCollapseFinishEvent?.Invoke();
        }
    }
}