namespace SerjBal
{
    internal class TemplatesProvider : ITemplatesProvider
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public TemplatesProvider(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public bool HasTamplates()
        {
            return false;
        }

        public ItemData SelectedTemplate { get; set; }
    }
}