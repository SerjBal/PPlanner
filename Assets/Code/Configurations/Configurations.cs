using TMPro;
using UnityEngine;

namespace SerjBal
{
    [CreateAssetMenu(fileName = "Configurations")]
    public class Configurations : ScriptableObject
    {
        public ButtonConfig buttonConfig;
        public IndicatorsConfig indicatorsConfig;
        private static Configurations _instance;

        public static Configurations Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<Configurations>("Configurations/Configurations");
                }
                return _instance;
            }
        }
    }
}