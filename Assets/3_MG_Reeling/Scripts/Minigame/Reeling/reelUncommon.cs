using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class reelUncommon : MonoBehaviour
{
    public GameObject jarum;
    public float rotationSpeed = 100f;
    public float damagePerSecond = 10f;
    public float maxFishHP = 100f;
    public Slider fishHPSlider;
    public Image damageAreaImage1;
    public Image damageAreaImage2; // Second damage area
    public float damageColorDuration = 0.5f;
    public float delayBeforeReactivatingDamageArea = 1f;
    public Slider timeSlider;
    public float maxTime = 60f;
    public Text timeText;

    private Image jarumImage;
    private float fishHP;
    private Color originalColor;
    private List<int> activeDamageAreas = new List<int>();
    private bool hasDamaged;
    private float currentTime;
    private Color originalSliderColor;

    void Start()
    {
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

        // Pastikan nilai rotasi z jarum berada dalam rentang 0 hingga 360 derajat
        Vector3 currentRotation = jarum.transform.eulerAngles;
        currentRotation.z = Mathf.Repeat(currentRotation.z, 360f);
        jarum.transform.eulerAngles = currentRotation;

        float currentAngle = currentRotation.z;
        bool isInDamageArea = false;
        int damageAreaIndex = -1;

        for (int i = 0; i < activeDamageAreas.Count; i++)
        {
            var area = GetAreaAngles(activeDamageAreas[i]);
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
                correctKeyPressed = IsCorrectKeyCombinationPressed(activeDamageAreas[damageAreaIndex]);
                incorrectKeyPressed = IsAnyIncorrectKeyPressed(activeDamageAreas[damageAreaIndex]);
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
                if (activeDamageAreas.Count == 0)
                {
                    StartCoroutine(ReactivateDamageAreaAfterDelay());
                }
            }
            else if (incorrectKeyPressed)
            {
                // Mengurangi waktu sebanyak 3 detik jika ada kesalahan
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

                // Reset hasDamaged to false if the wrong key is pressed
                hasDamaged = false;
            }
        }

        if (!isInDamageArea)
        {
            hasDamaged = false;
        }

        // Debugging logs
        Debug.Log($"Current Angle: {currentAngle}");
        Debug.Log($"Is In Damage Area: {isInDamageArea}");
        Debug.Log($"Damage Area Index: {damageAreaIndex}");
        Debug.Log($"Active Damage Areas: {string.Join(", ", activeDamageAreas)}");
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

    private bool IsAngleInRange(float angle, float startAngle, float endAngle)
    {
        return startAngle < endAngle ? angle >= startAngle && angle <= endAngle : angle >= startAngle || angle <= endAngle;
    }

    private void ActivateRandomDamageAreas()
    {
        activeDamageAreas.Clear();
        while (activeDamageAreas.Count < 2)
        {
            int newArea = UnityEngine.Random.Range(0, 6);
            if (!activeDamageAreas.Contains(newArea))
            {
                activeDamageAreas.Add(newArea);
            }
        }

        SetDamageAreaImage(damageAreaImage1, activeDamageAreas[0]);
        SetDamageAreaImage(damageAreaImage2, activeDamageAreas[1]);

        hasDamaged = false; // Reset hasDamaged here

        // Log the current damage areas
        Debug.Log($"Current damage areas: {string.Join(", ", activeDamageAreas)}");
    }

    private void SetDamageAreaImage(Image damageAreaImage, int areaIndex)
    {
        damageAreaImage.gameObject.SetActive(true);
        damageAreaImage.fillAmount = 1f / 6f; // Set fill amount to 1/6
        var area = GetAreaAngles(areaIndex);
        damageAreaImage.transform.rotation = Quaternion.Euler(0, 0, area.startAngle + 60f);
    }

    private (float startAngle, float endAngle) GetAreaAngles(int index)
    {
        return index switch
        {
            0 => (330f, 30f), // Mengubah posisi area damage untuk index 0
            1 => (30f, 90f),
            2 => (90f, 150f),
            3 => (150f, 210f),
            4 => (210f, 270f),
            5 => (270f, 330f),
            _ => (0f, 0f),
        };
    }

    private KeyCode[] GetKeyCodeForArea(int index)
    {
        return index switch
        {
            0 => new KeyCode[] { KeyCode.UpArrow },
            1 => new KeyCode[] { KeyCode.UpArrow, KeyCode.LeftArrow },
            2 => new KeyCode[] { KeyCode.LeftArrow, KeyCode.DownArrow },
            3 => new KeyCode[] { KeyCode.DownArrow },
            4 => new KeyCode[] { KeyCode.DownArrow, KeyCode.RightArrow },
            5 => new KeyCode[] { KeyCode.RightArrow, KeyCode.UpArrow },
            _ => new KeyCode[] { KeyCode.None },
        };
    }

    private void DeactivateDamageArea(int index)
    {
        if (index < activeDamageAreas.Count)
        {
            int areaIndex = activeDamageAreas[index];
            if (areaIndex == activeDamageAreas[0])
            {
                damageAreaImage1.gameObject.SetActive(false);
            }
            else if (areaIndex == activeDamageAreas[1])
            {
                damageAreaImage2.gameObject.SetActive(false);
            }
            activeDamageAreas.RemoveAt(index);

            // Reset hasDamaged to false after deactivating the damage area
            hasDamaged = false;

            // Log the deactivation
            Debug.Log($"Deactivated damage area: {areaIndex}");
        }
    }

    private IEnumerator ReactivateDamageAreaAfterDelay()
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
        // Mengubah warna slider menjadi merah
        if (timeSlider != null)
        {
            var sliderImage = timeSlider.fillRect.GetComponent<Image>();
            sliderImage.color = Color.red;
        }

        // Efek getar
        Vector3 originalPosition = timeSlider.transform.position;
        float shakeDuration = 0.2f;
        float shakeMagnitude = 5f;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;

            timeSlider.transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        timeSlider.transform.position = originalPosition;

        // Mengembalikan warna slider ke warna asli
        yield return new WaitForSeconds(0.5f);
        if (timeSlider != null)
        {
            var sliderImage = timeSlider.fillRect.GetComponent<Image>();
            sliderImage.color = originalSliderColor;
        }
    }

    private string FormatTime(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }
}