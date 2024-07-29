namespace Base.Architecture
{
    public interface IArchitecture
    {
        bool HasService<TService>() where TService : IService;
        TService GetService<TService>() where TService : IService;
        void RegisterService<TService>(TService service) where TService : IService;
        void UnregisterService<TService>() where TService : IService;
        void StartServices();
        void DisposeServices();
    }
}