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
                // Aktifkan GameObject
                if (scriptManager != null)
                {
                    scriptManager.SetActive(true);
                }
                else
                {
                    Debug.LogError("Object to activate is not assigned!");
                }
            }
        }
    }
}