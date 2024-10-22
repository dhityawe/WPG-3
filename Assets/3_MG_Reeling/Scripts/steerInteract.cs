using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MG_Reeling {
    public class SteerInteract : MonoBehaviour
    {
        public void Interact()
        {
            // Logika interaksi
            Debug.Log("Objek diinteraksi: " + gameObject.name);

            // Simpan posisi dan rotasi sebelum pindah scene
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Camera mainCamera = Camera.main;

            if (player != null && mainCamera != null)
            {
                PositionRotationManager.SavePlayerPositionAndRotation(player, mainCamera);
            }
            else
            {
                Debug.LogError("Player atau Camera tidak ditemukan!");
            }

            // Pindah scene menggunakan SceneLoader
            if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance._2DExplore(); // Ganti dengan metode yang sesuai
            }
            else
            {
                Debug.LogError("SceneLoader tidak ditemukan!");
            }
        }
    }
}