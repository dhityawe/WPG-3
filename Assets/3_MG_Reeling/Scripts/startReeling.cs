using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MG_Reeling {
    public class startReeling : MonoBehaviour
    {
        public GameObject scriptManager;

        void Update()
        {
            // Periksa apakah tombol spasi ditekan
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Ambil script state dari scriptManager
                state stateScript = scriptManager.GetComponent<state>();
                // Ubah state menjadi reelingState
                stateScript._reelingState();
            }
        }
    }
}