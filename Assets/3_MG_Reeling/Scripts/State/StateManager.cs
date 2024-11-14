using UnityEngine;

namespace MG_Reeling {
    public enum FishType {
        Common,
        Uncommon,
        Anomaly
    }

    public class StateManager : MonoBehaviour {
        public GameObject reelingPanel;
        public GameObject gachaPanel;
        public GameObject uiPanel;
        public ReelCommon reelCommonScript;
        public ReelUncommon reelUncommonScript;
        public reelAnomaly reelAnomalyScript;
        private reelingBase currentReelingBaseScript;
        public GachaSystem gachaSystemScript;

        public FishType fishType; // Add this line to allow setting fish type in Inspector

        private State currentState;

        void Start() {
            // Ensure all scripts are initially disabled
            reelCommonScript.enabled = false;
            reelUncommonScript.enabled = false;
            reelAnomalyScript.enabled = false;

            // Periksa status perpindahan scene
            if (PlayerPrefs.GetInt("HasVisitedEndlessRun", 0) == 1) {
                SwitchToReelingState();
            } else {
                SetState(new IdleState(this));
            }
        }

        void Update() {
            currentState?.Update();
        }

        public void SetState(State newState) {
            if (currentState != null) {
                currentState.Exit();
            }
            currentState = newState;
            currentState.Enter();
        }

        public void SwitchToIdleState() {
            SetState(new IdleState(this));
        }

        public void SwitchToReelingState() {
            Debug.Log("Switching to Reeling State with fish type: " + fishType);

            // Disable all reeling scripts first
            reelCommonScript.enabled = false;
            reelUncommonScript.enabled = false;
            reelAnomalyScript.enabled = false;

            // Determine which reelingBase script to use based on fishType
            switch (fishType) {
                case FishType.Common:
                    currentReelingBaseScript = reelCommonScript;
                    break;
                case FishType.Uncommon:
                    currentReelingBaseScript = reelUncommonScript;
                    break;
                case FishType.Anomaly:
                    currentReelingBaseScript = reelAnomalyScript;
                    break;
                default:
                    Debug.LogError("Invalid fish type selected.");
                    return;
            }

            if (currentReelingBaseScript != null) {
                Debug.Log("Current Reeling Base Script: " + currentReelingBaseScript.GetType().Name);
                currentReelingBaseScript.Initialize(); // Panggil metode Initialize sebelum mengaktifkan skrip
                currentReelingBaseScript.enabled = true;
                SetState(new ReelingState(this, currentReelingBaseScript));
            } else {
                Debug.LogError("No reelingBase script selected.");
            }
        }

        public void SwitchToGachaState() {
            SetState(new GachaState(this));
        }

        public reelingBase CurrentReelingBaseScript {
            get { return currentReelingBaseScript; }
            private set { currentReelingBaseScript = value; }
        }
    }
}