using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Tambahkan referensi ke SceneManager

namespace MG_2DExplore {
    public class boatInteract : MonoBehaviour
    {
        public string sceneName; // Nama scene yang akan dipindahkan

        void Update()
        {
            // Cek apakah tombol 'E' ditekan
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Pindah ke scene yang ditentukan
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}