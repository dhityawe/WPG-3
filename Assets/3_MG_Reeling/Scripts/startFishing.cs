using System.Collections;
using UnityEngine;

namespace MG_Reeling {
    public class startFishing : MonoBehaviour
    {
        private bool isPlayerInCollider = false;
        private Animator uiAnimator;

        void Start()
        {
            // Cari UI dan ambil komponen Animator
            GameObject fishingUI = GameObject.FindGameObjectWithTag("Rod");
            if (fishingUI != null)
            {
                uiAnimator = fishingUI.GetComponent<Animator>();
                if (uiAnimator == null)
                {
                    Debug.LogError("Animator tidak ditemukan pada Fishing UI!");
                }
            }
            else
            {
                Debug.LogError("Fishing UI tidak ditemukan!");
            }
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

                // Mulai Coroutine untuk animasi dan perpindahan scene
                StartCoroutine(PlayFishingAnimationAndLoadScene());
            }
        }

        private IEnumerator PlayFishingAnimationAndLoadScene()
        {
            if (uiAnimator != null)
            {
                // Mainkan animasi melempar joran pancing
                uiAnimator.SetTrigger("ThrowFishingRod");

                // Tunggu hingga animasi mulai diputar
                yield return new WaitUntil(() => uiAnimator.GetCurrentAnimatorStateInfo(0).IsName("ThrowRod"));

                // Ambil durasi animasi yang benar
                float animationLength = uiAnimator.GetCurrentAnimatorStateInfo(0).length;

                // Tunggu hingga animasi selesai
                yield return new WaitForSeconds(animationLength - 0.15f);

                // Log untuk memastikan animasi selesai
                Debug.Log("Animasi selesai, berpindah scene...");

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
            else
            {
                Debug.LogError("uiAnimator is null!");
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