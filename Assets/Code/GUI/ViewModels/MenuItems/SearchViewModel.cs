using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace SerjBal
{
    public class SearchViewModel : ItemViewModel
    {
        [SerializeField] private TMP_InputField inputField;
        private SearchEngine _searchEngine;
        private CancellationTokenSource _cancelationToken;

        public void Initialize(Services services, ButtonConfigs configs)
        {
            base.Initialize(configs);
            _searchEngine = new SearchEngine(services);
            Binds();
        }

        private void Binds() => inputField.onValueChanged.AddListener(delegate {ShowResults(); });

        public void ShowResults()
        {
            _cancelationToken?.Cancel();
            _cancelationToken = new CancellationTokenSource();

            if (inputField.text.Length > 0)
                DelayAndSearch(_cancelationToken.Token);
            else
                Close();
        }

        public override async void UpdateContent()
        {
            Childs.Clear();
            ContentContainer.Clear();
            
            var searchResults = _searchEngine.GetResults();
            for (int i = 0; i < searchResults.Count; i++)
            {
                Childs.Add(await _factory.CreateSearchResultItem(this, searchResults[i]));
            }
            animator.onExpandFinishEvent?.Invoke();
        }

        private async Task DelayAndSearch(CancellationToken token)
        {
            await Task.Delay(TimeSpan.FromSeconds(1), token);
            _searchEngine.Search(inputField.text, Open);
        }
    }
}