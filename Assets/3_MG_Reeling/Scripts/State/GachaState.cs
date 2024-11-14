using UnityEngine;
using System.Collections;

namespace MG_Reeling {
    public class GachaState : State {
        public GachaState(StateManager stateManager) : base(stateManager) { }

        public override void Enter() {
            stateManager.StartCoroutine(GachaCoroutine());
        }

        public override void Update() {
            // Implementasikan logika update untuk GachaState jika diperlukan
        }

        public override void Exit() {
            // Implementasikan logika exit untuk GachaState jika diperlukan
        }

        private IEnumerator GachaCoroutine() {
            Debug.Log("Gacha State");
            stateManager.reelingPanel.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            stateManager.gachaPanel.SetActive(true);
            stateManager.gachaSystemScript.StartSpin();
            stateManager.uiPanel.SetActive(false);
        }
    }
}