namespace Base.Core
{
    public abstract class State<TMachine> : IState where TMachine : StateMachine
    {
        protected TMachine entity;

        protected State(TMachine entity) {
            this.entity = entity;
        }

        public virtual void Enter() { }

        public virtual void Update() { }

        public virtual void Exit() { }
    }
}