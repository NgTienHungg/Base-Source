namespace Base.Core
{
    public abstract class State<TMachine> : IState where TMachine : StateMachine
    {
        protected TMachine owner;

        protected State(TMachine owner) {
            this.owner = owner;
        }

        public virtual void Enter() { }

        public virtual void Update() { }

        public virtual void Exit() { }
    }
}