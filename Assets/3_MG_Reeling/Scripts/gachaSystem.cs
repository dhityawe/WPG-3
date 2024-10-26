using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MG_Reeling {
    public class GachaSystem : MonoBehaviour {
        [System.Serializable]
        public class Fish {
            public string fishName;
            public Sprite fishSprite;
        }

        public GameObject gachaPanel;
        public List<Fish> fishes;
        public float spinDuration = 3.0f;
        public float initialSpinSpeed = 0.1f;
        public float finalSpinSpeed = 0.5f;
        public float constantSpinDuration = 1.0f;
        public Image leftContainer;
        public Image middleContainer;
        public Image rightContainer;

        private bool isSpinning = false;
        private float spinTime;
        private int currentIndex;
        private float spinSpeed;
        public GameObject stateManager;

        void Start() {
            InitializeContainers();
        }

        void Update() {
            if (isSpinning) {
                spinTime -= Time.deltaTime;
                if (spinTime <= 0) {
                    isSpinning = false;
                    StartCoroutine(StopSpin());
                }
            }
        }

        public void StartSpin() {
            if (!isSpinning) {
                spinTime = spinDuration;
                spinSpeed = initialSpinSpeed;
                isSpinning = true;
                StartCoroutine(MoveSprites());
            }
        }

        private void InitializeContainers() {
            if (fishes.Count >= 3) {
                leftContainer.sprite = fishes[0].fishSprite;
                middleContainer.sprite = fishes[1].fishSprite;
                rightContainer.sprite = fishes[2].fishSprite;
                currentIndex = 2;
            }
        }

        private IEnumerator MoveSprites() {
            float elapsedTime = 0f;

            while (isSpinning) {
                yield return new WaitForSeconds(spinSpeed);

                currentIndex = (currentIndex + 1) % fishes.Count;
                leftContainer.sprite = middleContainer.sprite;
                middleContainer.sprite = rightContainer.sprite;
                rightContainer.sprite = fishes[currentIndex].fishSprite;

                elapsedTime += spinSpeed;
                if (elapsedTime > constantSpinDuration) {
                    spinSpeed = Mathf.Lerp(initialSpinSpeed, finalSpinSpeed, (elapsedTime - constantSpinDuration) / (spinDuration - constantSpinDuration));
                }
            }
        }

        private IEnumerator StopSpin() {
            yield return new WaitForSeconds(1f);
            StartCoroutine(DetermineResult());
            yield return new WaitForSeconds(2f);
            StateManager stateManager = this.stateManager.GetComponent<StateManager>();
            stateManager.SwitchToIdleState();
        }

        private IEnumerator DetermineResult() {
            yield return new WaitForSeconds(2f);
        }
    }
}