using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class DayView : MonoBehaviour
    {
        public Button button;
        public TextMeshProUGUI nameText;
        public Image image;
        public Color nonExistDayColor;
        public Color existDayColor;

        public void SetState(bool isSaved)
        {
            image.color = isSaved ? existDayColor : nonExistDayColor;
        }
    }
}