using UnityEngine;

namespace Base
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected IState currentState;

        protected virtual void Update() {
            currentState?.Update();
        }

        public virtual void SetState(IState state) {
            currentState?.Exit();
            currentState = state;
            currentState.Enter();
        }
    }
}