using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class LoaderScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform loadingProgressBar;
        private bool _isCanceled;
        [SerializeField] float rotationSpeed = 500;

        public void Initialize()
        {
            Show(true);
            DontDestroyOnLoad(this);
        }

        public void Show(bool value)
        {
            if (value)
            {
                gameObject.SetActive(true);
                StartCoroutine(Animation());
            }
            else
                _isCanceled = true;
        }

        private IEnumerator Animation()
        {
            while (true)
            {
                loadingProgressBar.eulerAngles = new Vector3(0, 0, loadingProgressBar.eulerAngles.z - Time.deltaTime * rotationSpeed);
                if (_isCanceled)
                {
                    _isCanceled = false;
                    gameObject.SetActive(false);
                    yield break;
                }

                yield return null;
            }
        }
    }
}