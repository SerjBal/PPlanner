using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class LoaderScreen : MonoBehaviour, IProgress
    {
        [SerializeField] private Image loadingProgressBar;

        public float Progress
        {
            get => loadingProgressBar.fillAmount;
            set
            {
                SetActivity(value);
                loadingProgressBar.fillAmount = value;
            }
        }

        public void Initialize()
        {
            Progress = 0;
            DontDestroyOnLoad(this);
        }

        private void SetActivity(float value)
        {
            if (value >= 1 && gameObject.activeSelf)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);
        }
    }
}