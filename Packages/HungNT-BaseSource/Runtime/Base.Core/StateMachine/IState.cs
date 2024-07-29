namespace Base
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }
}