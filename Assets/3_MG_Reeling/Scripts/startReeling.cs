using UnityEngine;
using MG_Reeling;

namespace MG_Reeling {
    public class StartReeling : MonoBehaviour {
        public GameObject stateManager;

        void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                stateManager.GetComponent<StateManager>().SwitchToReelingState();
            }
        }
    }
}