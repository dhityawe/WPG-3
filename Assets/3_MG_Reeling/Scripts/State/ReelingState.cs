using UnityEngine;

namespace MG_Reeling {
    public class ReelingState : State {
        private reelingBase reelingBaseScript;

        public ReelingState(StateManager stateManager, reelingBase reelingBaseScript) : base(stateManager) {
            this.reelingBaseScript = reelingBaseScript;
        }

        public override void Enter() {
            stateManager.reelingPanel.SetActive(true);
            reelingBaseScript.BackgroundImage.gameObject.SetActive(true);
            stateManager.StartCoroutine(reelingBaseScript.UpdateTimer());
            reelingBaseScript.PublicActivateRandomDamageAreas();
            stateManager.uiPanel.SetActive(true);

            // Hapus status perpindahan scene
            PlayerPrefs.DeleteKey("HasVisitedEndlessRun");
        }

        public override void Update() {
            // Implementasikan logika update untuk ReelingState jika diperlukan
        }

        public override void Exit() {
            // Implementasikan logika exit untuk ReelingState jika diperlukan
        }
    }
}