using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SerjBal
{
    public class ButtonSwipeController: MonoBehaviour
    {
        [SerializeField] private RectTransform rightButtonsTransform;
        [SerializeField] private RectTransform leftButtonsTransform;
        [SerializeField] private ExpandAnimator animator;
        private float _onOnButtonClickTimer;
        private float _maxSliceDistance;
        private float _timer;
        private bool _isOnButtonClickAllowed;
        private bool _isExpaned;
        private Camera _camera;

        public void Initialize(IViewModel channel, ButtonConfigs configs)
        {
            _onOnButtonClickTimer = configs.clickTimer;
            _maxSliceDistance = configs.swipeDistance;
            
            animator.onExpandEvent = channel.OnExpand;
            animator.onCollapsedEvent = channel.OnCollapsed;
            animator.Initialize(configs.expandAnimationCurve);
            _camera = Camera.main;
        }
       
        public void OnPointerDown(PointerEventData eventData)
        {
            _timer = _onOnButtonClickTimer;
            StartCoroutine((IEnumerator)MoveOrClick());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isOnButtonClickAllowed)
            {
                Reset();
                if (_isExpaned) animator.PlayOpen();
                else animator.PlayClose();
                _isExpaned = !_isExpaned;
            }
            else
            {
                Debug.Log("Return button to near position");
            }
        }

        IEnumerator MoveOrClick()
        {
            _isOnButtonClickAllowed = true;
            _timer -= Time.deltaTime;
            while (_timer>0)
            {
                float currentPos = transform.localPosition.x;
                if (currentPos > _maxSliceDistance && currentPos < -_maxSliceDistance) _isOnButtonClickAllowed = false;
                yield return null;
            }
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            rightButtonsTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, -worldPosition.x);
            leftButtonsTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, worldPosition.x);
        }

        private void Reset()
        {
            StopCoroutine(MoveOrClick());
            _isOnButtonClickAllowed = false;
            float yPos = transform.localPosition.y;
            transform.localPosition = new Vector3(0,yPos,0);
        }

        private void OnDisable()
        {
            Reset();
        }
    }
}