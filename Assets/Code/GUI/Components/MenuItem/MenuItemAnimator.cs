using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class MenuItemAnimator : MonoBehaviour
    {
        public Action onExpandStartEvent;
        public Action onExpandFinishEvent;
        public Action onCollapseStartEvent;
        public Action onCollapseFinishEvent;
        [SerializeField] private ScrollRect contentScrollRect;
        [SerializeField] private RectTransform buttonTransform;
        private AnimationCurve _expandAnimationCurve;
        private bool _isExpaned;
        private float _buttonHeight;
        private IGUIModelView _gui;
        private float _yPos;
        private float _sizeB;
        private VerticalLayoutGroup _layout;

        public void Initialize(AnimationCurve expandAnimationCurve)
        {
            _layout = transform.parent.GetComponent<VerticalLayoutGroup>();
            _expandAnimationCurve = expandAnimationCurve;
            _gui = new Services().Single<IGUIModelView>();
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
            float maxTime = _expandAnimationCurve[_expandAnimationCurve.length - 1].time;
            while (timer<maxTime)
            {
                var t = _expandAnimationCurve.Evaluate(timer);   
                var x = buttonTransform.anchoredPosition.x;
                buttonTransform.anchoredPosition = new Vector2(x, Mathf.Lerp(_yPos, 0, t));
                buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(_buttonHeight, _sizeB, t));
                timer += Time.deltaTime;
                yield return null;
            }
            OnExpandFinish();
        }

        private IEnumerator Collapse()
        {
            OnCollapseStart();
            float timer = 0;
            float maxTime = _expandAnimationCurve[_expandAnimationCurve.length - 1].time;
            while (timer<maxTime)
            {
                var t = _expandAnimationCurve.Evaluate(timer);   
                var x = buttonTransform.anchoredPosition.x;
                buttonTransform.anchoredPosition = new Vector2(x, Mathf.Lerp(0, _yPos, t));
                buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(_sizeB, _buttonHeight, t));
                timer += Time.deltaTime;
                yield return null;
            }
            OnCollapseFinish();
        }

        private void OnExpandStart()
        {
            _gui.InteractonEnable(true);
            contentScrollRect.vertical = true;
            _yPos = buttonTransform.anchoredPosition.y;
            float parentYPositionInWorldSpace = buttonTransform.parent.position.y;
            _sizeB = parentYPositionInWorldSpace;
            if (_layout != null) _layout.enabled = false;
            onExpandStartEvent?.Invoke();
        }

        private void OnExpandFinish()
        {
            buttonTransform.anchoredPosition = new Vector2( buttonTransform.anchoredPosition.x, 0);
            buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _sizeB);
            _gui.InteractonEnable(false);
            onExpandFinishEvent?.Invoke();
        }

        private void OnCollapseStart()
        {
            contentScrollRect.vertical = false;
            _gui.InteractonEnable(true);
            onCollapseStartEvent?.Invoke();
        }
        
        private void OnCollapseFinish()
        {
            buttonTransform.anchoredPosition = new Vector2( buttonTransform.anchoredPosition.x, _yPos);
            buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _buttonHeight);
            _gui.InteractonEnable(false);
            if (_layout != null) _layout.enabled = true;
            onCollapseFinishEvent?.Invoke();
        }
    }
}