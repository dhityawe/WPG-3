using UnityEngine;

namespace MG_Reeling {
    public class StateManager : MonoBehaviour {
        public GameObject reelingPanel;
        public GameObject gachaPanel;
        public GameObject uiPanel;
        public reelingBase reelingBaseScript;
        public GachaSystem gachaSystemScript;

        private State currentState;

        void Start() {
            reelingBaseScript = FindObjectOfType<reelingBase>();
            if (reelingBaseScript == null) {
                Debug.LogError("reelingBase not found in the scene.");
                return;
            }
            SetState(new IdleState(this));
        }

        void Update() {
            currentState?.Update();
        }

        public void SetState(State newState) {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public void SwitchToIdleState() {
            SetState(new IdleState(this));
        }

        public void SwitchToReelingState() {
            SetState(new ReelingState(this));
        }

        public void SwitchToGachaState() {
            SetState(new GachaState(this));
        }
    }
}