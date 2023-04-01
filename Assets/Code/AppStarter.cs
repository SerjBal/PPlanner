using UnityEngine;

namespace SerjBal
{
    public class AppStarter : MonoBehaviour
    {
        public Bootloader bootloaderPrefab;

        private void Awake()
        {
            var bootstrapper = FindObjectOfType<Bootloader>();

            if (bootstrapper != null) return;

            Instantiate(bootloaderPrefab);
        }
    }
}