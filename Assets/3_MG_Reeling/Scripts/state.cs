using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MG_Reeling {
    public class state : MonoBehaviour
    {
        public GameObject reelingPanel;
        public GameObject gachaPanel;
        private reelingBase reelingBaseScript;
        public GachaSystem gachaSystemScript;

        void Start()
        {
            reelingBaseScript = GetComponent<reelingBase>();
        }

        public void _idleState()
        {
            reelingPanel.SetActive(false);
            gachaPanel.SetActive(false);
            reelingBaseScript.BackgroundImage.gameObject.SetActive(false);
            reelingBaseScript.DeactivateAllDamageAreas();
        }

        public void _reelingState()
        {
            reelingPanel.SetActive(true);
            reelingBaseScript.BackgroundImage.gameObject.SetActive(true);
            StartCoroutine(reelingBaseScript.UpdateTimer());
            reelingBaseScript.PublicActivateRandomDamageAreas();
        }

        public IEnumerator _gachaState()
        {
            Debug.Log("Gacha State");
            reelingPanel.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            gachaPanel.SetActive(true);
            gachaSystemScript.StartSpin();
        }
    }
}