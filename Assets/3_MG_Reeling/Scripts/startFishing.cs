using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MG_Reeling {
    public class startFishing : MonoBehaviour
    {
        private bool isPlayerInCollider = false;

        void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Camera mainCamera = Camera.main;

            // Load player and camera position and rotation
            PositionRotationManager.LoadPlayerPositionAndRotation(player, mainCamera);
        }

        // Update is called once per frame
        void Update()
        {
            if (isPlayerInCollider && Input.GetKeyDown(KeyCode.E))
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Camera mainCamera = Camera.main;

                // Save player and camera position and rotation
                PositionRotationManager.SavePlayerPositionAndRotation(player, mainCamera);

                // Load new scene
                if (SceneLoader.Instance != null)
                {
                    SceneLoader.Instance._MGEndlessRun();
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