using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class ExpandAnimator : MonoBehaviour
    {
        public Action onExpandEvent;
        public Action onCollapsedEvent;
        private AnimationCurve _expandAnimationCurve;
        private bool _isExpaned;
        private float _buttonHeight;
        [SerializeField]private RectTransform buttonTransform;
        private IGUIModelView _gui;
        private float _y;
        private float _sizeB;
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
        public void PlayClose() => StartCoroutine(Collapse());

        private void PlayOpen() => StartCoroutine(Expand());

        private IEnumerator Expand()
        {
            onExpandEvent.Invoke();
            _y = buttonTransform.localPosition.y;
            _gui.DisableMenuInteracton(false);
            _sizeB = _gui.GetMenuBounds();
            var layout = transform.parent.GetComponent<VerticalLayoutGroup>();
            layout.enabled = false;
            float timer = 0;
            while (timer<0.5f)
            {
                var pos = buttonTransform.localPosition;
                buttonTransform.localPosition = new Vector3(pos.x, Mathf.Lerp(_y, 0, timer), pos.z);
                buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(_buttonHeight, _sizeB, timer));
                timer += Time.deltaTime;
                yield return null;
            }
            buttonTransform.localPosition = new Vector3( buttonTransform.localPosition.x, _sizeB,  buttonTransform.localPosition.z);
            buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _sizeB);
            _gui.DisableMenuInteracton(true);
        }
        
        private IEnumerator Collapse()
        {
            var layout = transform.parent.GetComponent<VerticalLayoutGroup>();
            _gui.DisableMenuInteracton(false);
            float timer = 0;
            while (timer<0.5f)
            {
                var pos = buttonTransform.localPosition;
                buttonTransform.localPosition = new Vector3(pos.x, Mathf.Lerp(0, _y, timer), pos.z);
                buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(_sizeB, _buttonHeight, timer));
                timer += Time.deltaTime;
                yield return null;
            }
            buttonTransform.localPosition = new Vector3( buttonTransform.localPosition.x, _y,  buttonTransform.localPosition.z);
            buttonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _buttonHeight);
            layout.enabled = true;
            onCollapsedEvent.Invoke();
            _gui.DisableMenuInteracton(true);
        }
    }
}