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
        private AnimationCurve _expandAnimationCurve;
        [SerializeField] private ScrollRect contentScrollRect;
        private bool _isExpaned;
        private float _buttonHeight;
        [SerializeField]private RectTransform buttonTransform;
        private IGUIModelView _gui;
        private float _y;
        private float _sizeB;
        private VerticalLayoutGroup _layout;

        public void Initialize(AnimationCurve expandAnimationCurve)
        {
            _layout = transform.parent.GetComponent<VerticalLayoutGroup>();
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
        public void PlayClose() => StartCoroutine(Collapse());

        public void PlayOpen() => StartCoroutine(Expand());

        private void OnExpandStart()
        {
            _gui.DisableMenuInteracton(true);
            contentScrollRect.vertical = true;
            _y = buttonTransform.anchoredPosition.y;
            _sizeB = _gui.GetMenuBounds();
            if (_layout != null) _layout.enabled = false;
            onExpandStartEvent?.Invoke();
        }

        private void OnExpandFinish()
        {
            buttonTransform.anchoredPosition = new Vector2( buttonTransform.anchoredPosition.x, 0);
            buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _sizeB);
            _gui.DisableMenuInteracton(false);
            onExpandFinishEvent.Invoke();
        }

        private void OnCollapseStart()
        {
            contentScrollRect.vertical = false;
            _gui.DisableMenuInteracton(true);
            onCollapseStartEvent.Invoke();
        }
        
        private void OnCollapseFinish()
        {
            buttonTransform.anchoredPosition = new Vector2( buttonTransform.anchoredPosition.x, _y);
            buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _buttonHeight);
            _gui.DisableMenuInteracton(false);
            if (_layout != null) _layout.enabled = true;
            onCollapseFinishEvent?.Invoke();
        }

        private IEnumerator Expand()
        {
            OnExpandStart();
            float timer = 0;
            float maxT = _expandAnimationCurve[_expandAnimationCurve.length - 1].time;
            while (timer<maxT)
            {
                var t = _expandAnimationCurve.Evaluate(timer);   
                var x = buttonTransform.anchoredPosition.x;
                buttonTransform.anchoredPosition = new Vector2(x, Mathf.Lerp(_y, 0, t));
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
            float maxT = _expandAnimationCurve[_expandAnimationCurve.length - 1].time;
            while (timer<maxT)
            {
                var t = _expandAnimationCurve.Evaluate(timer);   
                var x = buttonTransform.anchoredPosition.x;
                buttonTransform.anchoredPosition = new Vector2(x, Mathf.Lerp(0, _y, t));
                buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(_sizeB, _buttonHeight, t));
                timer += Time.deltaTime;
                yield return null;
            }
           OnCollapseFinish();
        }
    }
}