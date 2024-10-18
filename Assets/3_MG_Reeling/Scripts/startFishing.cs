using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MG_Reeling {
    public class startFishing : MonoBehaviour
    {
        private bool isPlayerInCollider = false;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (isPlayerInCollider && Input.GetKeyDown(KeyCode.E))
            {
                if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance._MGEndlessRun(); // Ganti dengan metode yang sesuai
            }
            else
            {
                Debug.LogError("SceneLoader tidak ditemukan!");
            }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInCollider = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInCollider = false;
            }
        }
    }
}