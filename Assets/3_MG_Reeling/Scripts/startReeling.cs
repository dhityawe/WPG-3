using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MG_Reeling {
    public class startReeling : MonoBehaviour
    {
        public GameObject scriptManager;
        public GameObject reelingPanel;

        public void StartReeling()
        {
            scriptManager.SetActive(true);
            reelingPanel.SetActive(true);
        }

        public void StopReeling()
        {
            scriptManager.SetActive(false);
            reelingPanel.SetActive(false);
        }
    }
}