using TMPro;
using UnityEngine;

namespace SerjBal
{
    public class SearchResultItem : ItemViewModel, ISearchItem
    {
        [SerializeField] private TextMeshProUGUI text;
        public void Setup(TextData data)
        {
            text.text = data.text;
        }
        
        public override void OnExpandStart()
        {
            contentContainer.gameObject.SetActive(true);
            canvas.overrideSorting = true;
        }
        public override void OnExpandFinish() { }
        public override void OnCollapseStart() { }
        public override void OnCollapseFinish()
        {
            contentContainer.gameObject.SetActive(false);
            canvas.overrideSorting = false;
        }
    }
}
