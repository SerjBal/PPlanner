using System;
using SerjBal.Windows;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SerjBal
{
    public class ColorSelectorWindowView : WindowView, IPointerClickHandler
    {
        public Image selectedColorIndicator;
        public Image colorPalette;
        public Action<Color> onColorSelected;
        public Color IndicatorColor
        {
            set
            {
                selectedColorIndicator.color = value;
                onColorSelected?.Invoke(value);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var localPoint = GetClickPosition(eventData);
            var pixelColor = GetPixelColor(localPoint);
            IndicatorColor = pixelColor;
        }

        private Color GetPixelColor(Vector2 localPoint)
        {
            var texture = colorPalette.sprite.texture;
            Color pixelColor = texture.GetPixel(
                (int)localPoint.x + texture.width / 2,
                (int)localPoint.y + texture.height / 2
            );
            return pixelColor;
        }

        private Vector2 GetClickPosition(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                colorPalette.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out var localPoint
            );
            return localPoint;
        }
    }
}
