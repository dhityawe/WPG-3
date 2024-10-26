using UnityEngine;

namespace MG_Reeling {
    public class IdleState : State {
        public IdleState(StateManager stateManager) : base(stateManager) { }

        public override void Enter() {
            stateManager.reelingPanel.SetActive(false);
            stateManager.gachaPanel.SetActive(false);
            stateManager.reelingBaseScript.BackgroundImage.gameObject.SetActive(false);
            stateManager.reelingBaseScript.DeactivateAllDamageAreas();
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