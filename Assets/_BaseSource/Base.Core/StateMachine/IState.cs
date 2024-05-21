namespace Base.Core
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }
}