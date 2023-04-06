using UnityEngine;

namespace SerjBal
{
    [CreateAssetMenu(menuName = "ButtonConfigs")]
    public class ButtonConfig : ScriptableObject
    {
        public float clickTimer;
        public float swipeDistance;
        public AnimationCurve expandAnimationCurve;
    }
}