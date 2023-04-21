using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerjBal
{
    [CreateAssetMenu(fileName = "IndicatorsConfig")]
    public class IndicatorsConfig : ScriptableObject
    {
        public Color contentPostUndoneColor;
        public Color contentPostDoneColor;
        public Color adsPostUndoneColor;
        public Color adsPostDoneColor;
        public Color timeProgressBarColor;
    }
}
