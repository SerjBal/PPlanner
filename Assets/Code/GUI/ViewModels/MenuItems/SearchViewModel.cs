using TMPro;
using UnityEngine;

namespace SerjBal
{
    public class SearchViewModel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        private Search _search;
        public void Initialize()
        {
            _search = new Search();
        }
    }
}