namespace Base.Core
{
    public interface IService
    {
        void OnInit();
        void OnStart();
        void OnUpdate();
        void OnDispose();
    }
}