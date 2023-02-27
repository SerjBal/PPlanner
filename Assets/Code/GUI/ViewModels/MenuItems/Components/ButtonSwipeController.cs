using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;

namespace SerjBal
{
    public class ButtonSwipeController: MonoBehaviour, IPointerDownHandler, IPointerExitHandler
    {
        public Action onSelectedEvent;
        [SerializeField] private RectTransform frontButtonTransform;
        [SerializeField] private RectTransform rightButtonsTransform;
        private float _onOnButtonClickTimer;
        private float _maxSliceDistance;
        private float _timer;
        private bool _isOnButtonClickAllowed;
        private bool _isOnPointerDown;
        private Vector3 _mouseStartPosition;
        private float frontMaxX;
        private float frontMinX;
        private float _width;

        public void Initialize(ButtonConfigs configs)
        {
            _width = frontButtonTransform.rect.width/2;
            _onOnButtonClickTimer = configs.clickTimer;
            _maxSliceDistance = configs.swipeDistance;
        }
       
        public void OnPointerDown(PointerEventData eventData)
        {
            _mouseStartPosition = Input.mousePosition;
            _timer = _onOnButtonClickTimer;
            _isOnPointerDown = true;
            _isOnButtonClickAllowed = true;
            frontMaxX = frontButtonTransform.offsetMax.x;
            frontMinX = frontButtonTransform.offsetMin.x;
        }

        private void Update()
        {
            if (_isOnPointerDown)
            {
                Vector3 mousePosition = _mouseStartPosition - Input.mousePosition;
                float offset = mousePosition.x * -1;
                ButtonResize(offset);
                ClickDisallow(offset);
                
                if (Input.GetMouseButtonUp(0)) OnPointerUp();
            }
        }
        
        public void OnPointerUp()
        {
            _isOnPointerDown = false;
            if (_isOnButtonClickAllowed)
            {
                Reset();
                onSelectedEvent?.Invoke();
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
            var offsetMax = new Vector2(Mathf.Clamp(frontMaxX + offset, -_width, 0), frontButtonTransform.offsetMax.y);
            frontButtonTransform.offsetMax = offsetMax;
            frontButtonTransform.offsetMin = new Vector2(0, frontButtonTransform.offsetMin.y);
                
            rightButtonsTransform.offsetMin = new Vector2(Mathf.Clamp(frontMaxX + offset, -_width, 0), rightButtonsTransform.offsetMax.y);
        }


        private IEnumerator SoftSnapp()
        {
            float time = 0;
            var frontOffsetMin = frontButtonTransform.offsetMin.y;
            var fronOffSetMax = frontButtonTransform.offsetMax.y;
            var minA = frontButtonTransform.offsetMin.x;
            var maxA = frontButtonTransform.offsetMax.x;
            var minCheck = minA > _width / 2;
            var maxCheck = maxA < -_width / 2;
            
            var minB = minCheck ? _width : 0;
            var maxB = maxCheck ? -_width : 0;
            
            while (time<1f)
            {
                var offsetMin = new Vector2(Mathf.Lerp(minA, minB, time), frontOffsetMin);
                var offsetMax = new Vector2(Mathf.Lerp(maxA, maxB, time), fronOffSetMax);
                var offsetRightMin = new Vector2(Mathf.Lerp(maxA, maxB, time), rightButtonsTransform.offsetMin.y);
                frontButtonTransform.offsetMin = offsetMin;
                frontButtonTransform.offsetMax = offsetMax;
                rightButtonsTransform.offsetMin = offsetRightMin;
                time += Time.deltaTime*10;
                yield return null;
            }
            frontButtonTransform.offsetMin = new Vector2(minB, frontOffsetMin);
            frontButtonTransform.offsetMax = new Vector2(maxB, fronOffSetMax);
            rightButtonsTransform.offsetMin = new Vector2(maxB, rightButtonsTransform.offsetMin.y);
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