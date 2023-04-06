using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerjBal
{
    [CreateAssetMenu(fileName = "IndicatorsConfig")]
    public class IndicatorsConfig : ScriptableObject
    {
        public Color contentPostActiveColor;
        public Color contentPostInactiveColor;
        public Color adsPostActiveColor;
        public Color adsPostInactiveColor;
        public Color timeProgressBarColor;
    }
}
