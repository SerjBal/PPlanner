using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;

namespace SerjBal
{
    public class ButtonSwipeController: MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerExitHandler
    {
        public Action onSelectedEvent;
        [SerializeField] private RectTransform frontButtonTransform;
        [SerializeField] private RectTransform rightButtonsTransform;
        [SerializeField] private Button button;
        private float _onOnButtonClickTimer;
        private float _maxSliceDistance;
        private float _timer;
        private bool _isOnButtonClickAllowed;
        private bool _isOnPointerDown;
        private Vector3 _mouseStartPosition;

        public void Initialize(ButtonConfigs configs)
        {
            _onOnButtonClickTimer = configs.clickTimer;
            _maxSliceDistance = configs.swipeDistance;
            button.onClick.AddListener(OnPointerUp);
        }
       
        public void OnPointerDown(PointerEventData eventData)
        {
            _mouseStartPosition = Input.mousePosition;
            _timer = _onOnButtonClickTimer;
            _isOnPointerDown = true;
            _isOnButtonClickAllowed = true;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (_isOnPointerDown)
            {
                Vector3 mousePosition = _mouseStartPosition - Input.mousePosition;
                float offset = mousePosition.x * -1;
                ButtonResize(offset);
                ClickDisallow(offset);
            }
        }
        
        public void OnPointerUp()
        {
            _isOnPointerDown = false;
            if (_isOnButtonClickAllowed)
            {
                Reset();
                onSelectedEvent.Invoke();
            }
            else
            {
                StartCoroutine(SoftSnapp());
            }
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isOnPointerDown) StartCoroutine(SoftSnapp());
            _isOnPointerDown = false;
        }
        private void ClickDisallow(float offset)
        {
            if (_timer > 0 && (offset > _maxSliceDistance || offset < -_maxSliceDistance))
            {
                _isOnButtonClickAllowed = false;
                _timer -= Time.deltaTime;
            }
        }

        private void ButtonResize(float offset)
        {
            float width = frontButtonTransform.rect.width / 2;
            if (offset > 0)
            {
                // var offsetMin = new Vector2(Mathf.Clamp(offset, 0, width), frontButtonTransform.offsetMin.y);
                // frontButtonTransform.offsetMin = offsetMin;
                // frontButtonTransform.offsetMax = new Vector2(0, frontButtonTransform.offsetMax.y);
            }
            else
            {
                var offsetMax = new Vector2(Mathf.Clamp(offset, -width, 0), frontButtonTransform.offsetMax.y);
                frontButtonTransform.offsetMax = offsetMax;
                frontButtonTransform.offsetMin = new Vector2(0, frontButtonTransform.offsetMin.y);
                
                rightButtonsTransform.offsetMin = new Vector2(Mathf.Clamp(offset, -width, 0), rightButtonsTransform.offsetMax.y);
            }
        }


        private IEnumerator SoftSnapp()
        {
            float time = 0;
            var width = frontButtonTransform.rect.width/2;
            var minA = frontButtonTransform.offsetMin.x;
            var maxA = frontButtonTransform.offsetMax.x;
            var minCheck = minA > width / 2;
            var maxCheck = maxA < -width / 2;
            
            var minB = minCheck ? width : 0;
            var maxB = maxCheck ? -width : 0;
            
            while (time<1f)
            {
                var offsetMin = new Vector2(Mathf.Lerp(minA, minB, time), frontButtonTransform.offsetMin.y);
                var offsetMax = new Vector2(Mathf.Lerp(maxA, maxB, time), frontButtonTransform.offsetMax.y);
                frontButtonTransform.offsetMin = offsetMin;
                frontButtonTransform.offsetMax = offsetMax;
                var offsetRightMin = new Vector2(Mathf.Lerp(maxA, maxB, time), rightButtonsTransform.offsetMin.y);
                rightButtonsTransform.offsetMin = offsetRightMin;
                time += Time.deltaTime*10;
                yield return null;
            }
        }
        private void Reset()
        {
            _isOnButtonClickAllowed = false;
            frontButtonTransform.offsetMin = new Vector2(0,frontButtonTransform.offsetMin.y);
            frontButtonTransform.offsetMax = new Vector2(0,frontButtonTransform.offsetMax.y);
            rightButtonsTransform.offsetMin = new Vector2(0, rightButtonsTransform.offsetMax.y);
        }
        
        private void OnDisable()
        {
            Reset();
        }
    }
}