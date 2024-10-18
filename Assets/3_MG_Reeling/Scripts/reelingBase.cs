using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using TMPro;

namespace MG_Reeling {
    public abstract class reelingBase : MonoBehaviour
    {
        public GameObject jarum;
        public float rotationSpeed = 100f;
        public float damagePerSecond = 10f;
        public float maxFishHP = 100f;
        public Slider fishHPSlider;
        public float damageColorDuration = 0.5f;
        public float delayBeforeReactivatingDamageArea = 1f;
        public Slider timeSlider;
        public float maxTime = 60f;
        public TMP_Text timeText; // Change Text to TMP_Text
        public Image backgroundImage;
        public GameObject reelingPanel;
        public GameObject gachaPanel;

        protected Image jarumImage;
        protected float fishHP;
        protected Color originalColor;
        protected float currentTime;
        protected Color originalSliderColor;
        protected bool isShaking;
        protected bool hasDamaged;

        protected abstract void ActivateRandomDamageAreas();
        protected abstract void SetDamageAreaImage(Image damageAreaImage, int areaIndex);
        protected abstract (float startAngle, float endAngle) GetAreaAngles(int index);
        protected abstract KeyCode[] GetKeyCodeForArea(int index);
        protected abstract void DeactivateDamageArea(int index);

        void Start()
        {
            reelingPanel.SetActive(true);
            backgroundImage.gameObject.SetActive(true);

            jarumImage = jarum.GetComponent<Image>();
            if (jarumImage == null)
            {
                Debug.LogError("Tidak ada komponen Image pada GameObject jarum.");
                return;
            }
            originalColor = jarumImage.color;

            fishHP = maxFishHP;
            if (fishHPSlider != null)
            {
                fishHPSlider.maxValue = maxFishHP;
                fishHPSlider.value = fishHP;
            }

            if (timeSlider != null)
            {
                timeSlider.maxValue = maxTime;
                timeSlider.value = maxTime;
                originalSliderColor = timeSlider.fillRect.GetComponent<Image>().color;
            }

            currentTime = maxTime;
            if (timeText != null)
            {
                timeText.text = FormatTime(currentTime);
            }

            StartCoroutine(UpdateTimer());
            ActivateRandomDamageAreas();
        }

        void Update()
        {
            jarum.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            Vector3 currentRotation = jarum.transform.eulerAngles;
            currentRotation.z = Mathf.Repeat(currentRotation.z, 360f);
            jarum.transform.eulerAngles = currentRotation;

            float currentAngle = currentRotation.z;
            bool isInDamageArea = false;
            int damageAreaIndex = -1;

            for (int i = 0; i < GetActiveDamageAreas().Count; i++)
            {
                var area = GetAreaAngles(GetActiveDamageAreas()[i]);
                if (IsAngleInRange(currentAngle, area.startAngle, area.endAngle))
                {
                    isInDamageArea = true;
                    damageAreaIndex = i;
                    break;
                }
            }

            if (Input.anyKeyDown)
            {
                bool correctKeyPressed = false;
                bool incorrectKeyPressed = false;

                if (isInDamageArea)
                {
                    correctKeyPressed = IsCorrectKeyCombinationPressed(GetActiveDamageAreas()[damageAreaIndex]);
                    incorrectKeyPressed = IsAnyIncorrectKeyPressed(GetActiveDamageAreas()[damageAreaIndex]);
                }
                else
                {
                    incorrectKeyPressed = IsAnyKeyPressed();
                }

                if (correctKeyPressed && !hasDamaged)
                {
                    fishHP -= damagePerSecond;
                    fishHP = Mathf.Max(fishHP, 0);
                    if (fishHPSlider != null)
                    {
                        fishHPSlider.value = fishHP;
                    }

                    hasDamaged = true;
                    DeactivateDamageArea(damageAreaIndex);
                    StartCoroutine(ChangeNeedleColorBriefly());
                    if (GetActiveDamageAreas().Count == 0)
                    {
                        StartCoroutine(ReactivateDamageAreaAfterDelay());
                    }
                }
                else if (incorrectKeyPressed)
                {
                    currentTime -= 3f;
                    currentTime = Mathf.Max(currentTime, 0);
                    if (timeSlider != null)
                    {
                        timeSlider.value = currentTime;
                    }
                    if (timeText != null)
                    {
                        timeText.text = FormatTime(currentTime);
                    }
                    StartCoroutine(ShowErrorEffect());
                    hasDamaged = false;
                }
            }

            if (!isInDamageArea)
            {
                hasDamaged = false;
            }

            if (fishHP <= 0 || currentTime <= 0)
            {
                StartCoroutine(StopGame());
            }
        }

        private bool IsCorrectKeyCombinationPressed(int activeDamageArea)
        {
            KeyCode[] correctKeys = GetKeyCodeForArea(activeDamageArea);
            return correctKeys.All(key => Input.GetKey(key));
        }

        private bool IsAnyIncorrectKeyPressed(int activeDamageArea)
        {
            KeyCode[] correctKeys = GetKeyCodeForArea(activeDamageArea);
            KeyCode[] allKeys = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
            return allKeys.Any(key => Input.GetKeyDown(key) && !correctKeys.Contains(key)) || correctKeys.Any(key => !Input.GetKey(key) && Input.GetKeyDown(key));
        }

        private bool IsAnyKeyPressed()
        {
            KeyCode[] allKeys = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
            return allKeys.Any(key => Input.GetKeyDown(key));
        }

        private IEnumerator UpdateTimer()
        {
            while (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                currentTime = Mathf.Max(currentTime, 0);

                if (timeSlider != null)
                {
                    timeSlider.value = currentTime;
                }

                if (timeText != null)
                {
                    timeText.text = FormatTime(currentTime);
                }

                yield return null;
            }
        }

        private string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time - minutes * 60);
            return string.Format("{0:0}:{1:00}", minutes, seconds);
        }

        private bool IsAngleInRange(float angle, float startAngle, float endAngle)
        {
            return startAngle < endAngle ? angle >= startAngle && angle <= endAngle : angle >= startAngle || angle <= endAngle;
        }

        public IEnumerator ReactivateDamageAreaAfterDelay()
        {
            yield return new WaitForSeconds(delayBeforeReactivatingDamageArea);
            ActivateRandomDamageAreas();
        }

        private IEnumerator ChangeNeedleColorBriefly()
        {
            if (jarumImage != null)
            {
                jarumImage.color = Color.red;
            }

            yield return new WaitForSeconds(damageColorDuration);

            if (jarumImage != null)
            {
                jarumImage.color = originalColor;
            }
        }

        private IEnumerator ShowErrorEffect()
        {
            if (isShaking)
            {
                yield break;
            }

            isShaking = true;

            if (timeSlider != null)
            {
                var sliderImage = timeSlider.fillRect.GetComponent<Image>();
                sliderImage.color = Color.red;
            }

            if (jarumImage != null)
            {
                jarumImage.color = Color.red;
            }

            if (backgroundImage != null)
            {
                backgroundImage.color = Color.red;
            }

            Vector3 originalTimeSliderPosition = timeSlider.transform.localPosition;
            Vector3 originalJarumPosition = jarum.transform.localPosition;
            Vector3 originalTimeTextPosition = timeText.transform.localPosition;
            Vector3 originalBackgroundPosition = backgroundImage.transform.localPosition;

            float shakeDuration = 0.2f;
            float shakeMagnitude = 5f;
            float elapsed = 0f;

            while (elapsed < shakeDuration)
            {
                float x = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;
                float y = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;

                timeSlider.transform.localPosition = originalTimeSliderPosition + new Vector3(x, y, 0);
                jarum.transform.localPosition = originalJarumPosition + new Vector3(x, y, 0);
                timeText.transform.localPosition = originalTimeTextPosition + new Vector3(x, y, 0);
                backgroundImage.transform.localPosition = originalBackgroundPosition + new Vector3(x, y, 0);

                elapsed += Time.deltaTime;

                yield return null;
            }

            timeSlider.transform.localPosition = originalTimeSliderPosition;
            jarum.transform.localPosition = originalJarumPosition;
            timeText.transform.localPosition = originalTimeTextPosition;
            backgroundImage.transform.localPosition = originalBackgroundPosition;

            yield return new WaitForSeconds(0.5f);
            if (timeSlider != null)
            {
                var sliderImage = timeSlider.fillRect.GetComponent<Image>();
                sliderImage.color = originalSliderColor;
            }

            if (jarumImage != null)
            {
                jarumImage.color = originalColor;
            }

            if (backgroundImage != null)
            {
                backgroundImage.color = Color.white;
            }

            isShaking = false;
        }

        protected abstract List<int> GetActiveDamageAreas();

        private IEnumerator StopGame()
        {
            if (fishHP <= 0)
            {
                Debug.Log("Game Over! Kamu berhasil menangkap ikan.");
                reelingPanel.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                gachaPanel.SetActive(true);
            }
            else
            {
                Debug.Log("Game Over! Waktu habis.");
            }
        }
    }
}