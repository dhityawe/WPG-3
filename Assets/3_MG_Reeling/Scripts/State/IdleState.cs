using UnityEngine;

namespace MG_Reeling {
    public class IdleState : State {
        public IdleState(StateManager stateManager) : base(stateManager) { }

        public override void Enter() {
            stateManager.reelingPanel.SetActive(false);
            stateManager.gachaPanel.SetActive(false);

            if (stateManager.CurrentReelingBaseScript != null) {
                stateManager.CurrentReelingBaseScript.BackgroundImage.gameObject.SetActive(false);
                stateManager.CurrentReelingBaseScript.DeactivateAllDamageAreas();
                stateManager.CurrentReelingBaseScript.enabled = false; // Disable the current reeling script
            } else {
                Debug.LogError("currentReelingBaseScript is null.");
            }

            stateManager.uiPanel.SetActive(true);
        }

        public override void Update() {
            // Implementasikan logika update untuk IdleState jika diperlukan
        }

        public override void Exit() {
            // Implementasikan logika exit untuk IdleState jika diperlukan
        }
    }
}