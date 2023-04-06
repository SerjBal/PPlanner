using UnityEngine;

namespace SerjBal
{
    [CreateAssetMenu(fileName = "Configurations")]
    public class Configurations : ScriptableObject
    {
        public ButtonConfig buttonConfig;
        public IndicatorsConfig indicatorsConfig;
    }
}