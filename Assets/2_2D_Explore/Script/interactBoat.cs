using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Tambahkan referensi ke SceneManager

namespace MG_2DExplore {
    public class boatInteract : MonoBehaviour
    {

        void Update()
        {
            // Cek apakah tombol 'E' ditekan
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Pindah ke scene yang ditentukan
            if (SceneLoader.Instance != null)
                {
                    SceneLoader.Instance._MGReeling(); // Ganti dengan metode yang sesuai
                }
            else
                {
                    Debug.LogError("SceneLoader tidak ditemukan!");
                }
            }
        }
    }
}