using System.Collections;
using UnityEngine;

namespace SerjBal
{
    public class FadeScreen : MonoBehaviour
    {
        public CanvasGroup curtain;

        public void Initialize()
        {
            curtain.alpha = 1;
            DontDestroyOnLoad(this);
        }

        public void Show(bool show, float time = 0)
        {
            gameObject.SetActive(true);
            var speed = 0.1f / time;
            StopAllCoroutines();
            curtain.alpha = show ? 0 : 1;
            if (show)
                StartCoroutine(FadeOut(speed));
            else
                StartCoroutine(FadeIn(speed));
        }

        private IEnumerator FadeIn(float speed)
        {
            while (curtain.alpha > 0)
            {
                curtain.alpha -= speed;
                yield return null;
            }

            gameObject.SetActive(false);
        }

        private IEnumerator FadeOut(float speed)
        {
            while (curtain.alpha < 1)
            {
                curtain.alpha += speed;
                yield return null;
            }
        }
    }
}