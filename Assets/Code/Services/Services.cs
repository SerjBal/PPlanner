namespace SerjBal
{
    public class Services
    {
        private static Services _instance;
        public static Services Container => _instance ?? (_instance = new Services());

        public void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            Implementation<TService>.ServiceInstance = implementation;
        }

        public TService Single<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }


        private class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}