using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SerjBal
{
    public class SwipeController : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
    {
        [SerializeField] private bool isSwipeEnable = true;
        [SerializeField] private RectTransform frontButtonTransform;
        [SerializeField] private RectTransform rightButtonsTransform;
        private bool _isOnButtonClickAllowed;
        private bool _isOnPointerDown;
        private float _maxSliceDistance;
        private Vector3 _mouseStartPosition;
        private float _onOnButtonClickTimer;
        private float _timer;
        private float _width;
        private float frontMaxX;
        private float frontMinX;
        public Action onSelectedEvent;

        private void Reset()
        {
            _isOnButtonClickAllowed = false;
            frontButtonTransform.offsetMin = new Vector2(0, frontButtonTransform.offsetMin.y);
            frontButtonTransform.offsetMax = new Vector2(0, frontButtonTransform.offsetMax.y);
            rightButtonsTransform.offsetMin = new Vector2(0, rightButtonsTransform.offsetMax.y);
        }


        private void Update()
        {
            if (_isOnPointerDown)
            {
                if (isSwipeEnable)
                {
                    var mousePosition = _mouseStartPosition - Input.mousePosition;
                    var offset = mousePosition.x * -1;

                    ButtonResize(offset);
                    ClickDisallow(offset);
                }

                if (Input.GetMouseButtonUp(0)) OnPointerUp();
            }
        }

        private void OnDisable()
        {
            Reset();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isOnPointerDown = true;
            _mouseStartPosition = Input.mousePosition;
            _timer = _onOnButtonClickTimer;
            _isOnButtonClickAllowed = true;
            frontMaxX = frontButtonTransform.offsetMax.x;
            frontMinX = frontButtonTransform.offsetMin.x;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isOnPointerDown) OnPointerUp();
        }

        public void Initialize(ButtonConfig config)
        {
            _width = frontButtonTransform.rect.width / 2;
            _onOnButtonClickTimer = config.clickTimer;
            _maxSliceDistance = config.swipeDistance;
        }

        private async Task Snap()
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

            while (time < 1f)
            {
                var offsetMin = new Vector2(Mathf.Lerp(minA, minB, time), frontOffsetMin);
                var offsetMax = new Vector2(Mathf.Lerp(maxA, maxB, time), fronOffSetMax);
                var offsetRightMin = new Vector2(Mathf.Lerp(maxA, maxB, time), rightButtonsTransform.offsetMin.y);
                frontButtonTransform.offsetMin = offsetMin;
                frontButtonTransform.offsetMax = offsetMax;
                rightButtonsTransform.offsetMin = offsetRightMin;
                time += Time.deltaTime * 10;
                await Task.Yield();
            }

            frontButtonTransform.offsetMin = new Vector2(minB, frontOffsetMin);
            frontButtonTransform.offsetMax = new Vector2(maxB, fronOffSetMax);
            rightButtonsTransform.offsetMin = new Vector2(maxB, rightButtonsTransform.offsetMin.y);
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
                Snap();
            }
        }

        private void ClickDisallow(float offset)
        {
            if (offset > _maxSliceDistance || offset < -_maxSliceDistance)
            {
                if (_timer > 0)
                    _timer -= Time.deltaTime;
                else
                    _isOnButtonClickAllowed = false;
            }
        }

        private void ButtonResize(float offset)
        {
            var offsetMax = new Vector2(Mathf.Clamp(frontMaxX + offset, -_width, 0), frontButtonTransform.offsetMax.y);
            frontButtonTransform.offsetMax = offsetMax;
            frontButtonTransform.offsetMin = new Vector2(0, frontButtonTransform.offsetMin.y);

            rightButtonsTransform.offsetMin = new Vector2(Mathf.Clamp(frontMaxX + offset, -_width, 0),
                rightButtonsTransform.offsetMax.y);
        }
    }
}