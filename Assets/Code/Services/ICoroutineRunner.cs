using System.Collections;
using UnityEngine;

namespace SerjBal
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}