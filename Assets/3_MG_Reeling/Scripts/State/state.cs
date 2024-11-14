using UnityEngine;

namespace MG_Reeling {
    public abstract class State {
        protected StateManager stateManager;

        public State(StateManager stateManager) {
            this.stateManager = stateManager;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}