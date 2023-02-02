using System;
using System.Collections;
using UnityEditorInternal;
using UnityEngine;

namespace SerjBal
{
    public class ExpandAnimator : MonoBehaviour
    {
        public Transform channelTransform;
        public Action onExpandEvent;
        public Action onCollapsedEvent;
        private AnimationCurve _expandAnimationCurve;

        public void Initialize(AnimationCurve expandAnimationCurve)
        {
            _expandAnimationCurve = expandAnimationCurve;
        }
        public void PlayClose()
        {
            StartCoroutine(Collapse());
        }

        public void PlayOpen()
        {
            StartCoroutine(Expand());
        }

        private IEnumerator Expand()
        {
            Debug.Log("Play expand animation");
            onExpandEvent.Invoke();
            yield break;
        }
        
        private IEnumerator Collapse()
        {
            Debug.Log("Play collapse animation");
            onCollapsedEvent.Invoke();
            yield break;
        }
    }
}