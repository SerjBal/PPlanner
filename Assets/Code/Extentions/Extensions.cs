using UnityEngine;

namespace SerjBal
{
    public static class Extensions
    {
        public static void Add(this Transform container, GameObject gameObject)
        {
            gameObject.transform.SetParent(container, false);
        }
    }
}