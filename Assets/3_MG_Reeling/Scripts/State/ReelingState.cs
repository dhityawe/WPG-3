using UnityEngine;

namespace MG_Reeling {
    public class ReelingState : State {
        public ReelingState(StateManager stateManager) : base(stateManager) { }

        public override void Enter() {
            stateManager.reelingPanel.SetActive(true);
            stateManager.reelingBaseScript.BackgroundImage.gameObject.SetActive(true);
            stateManager.StartCoroutine(stateManager.reelingBaseScript.UpdateTimer());
            stateManager.reelingBaseScript.PublicActivateRandomDamageAreas();
            stateManager.uiPanel.SetActive(true);
        }

        public override void Update() {
            // Implementasikan logika update untuk ReelingState jika diperlukan
        }

        public override void Exit() {
            // Implementasikan logika exit untuk ReelingState jika diperlukan
        }
    }
}