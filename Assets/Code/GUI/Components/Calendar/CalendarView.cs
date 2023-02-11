using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class CalendarView : MonoBehaviour
    {
        public TMP_Dropdown yearDropdown;
        public ScrollRect scrollRect;
        public MonthView[] months;
    }
}
