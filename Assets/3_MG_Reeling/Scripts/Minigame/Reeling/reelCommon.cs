using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class reelCommon : MonoBehaviour
{
    public GameObject jarum;
    public float rotationSpeed = 100f;
    public float damagePerSecond = 10f;
    public float maxFishHP = 100f;
    public Slider fishHPSlider;
    public Image damageAreaImage; // Menggunakan satu Image untuk area damage
    public float damageColorDuration = 0.5f;
    public float delayBeforeReactivatingDamageArea = 1f;
    public Slider timeSlider;
    public float maxTime = 60f;
    public Text timeText;

    private Image jarumImage;
    private float fishHP;
    private Color originalColor;
    private int activeDamageArea;
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
        ActivateRandomDamageArea();
    }

    void Update()
    {
        jarum.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        float currentAngle = jarum.transform.eulerAngles.z;
        var area = GetAreaAngles(activeDamageArea);
        bool isInDamageArea = IsAngleInRange(currentAngle, area.startAngle, area.endAngle);

        if (Input.anyKeyDown)
        {
            KeyCode correctKey = GetKeyCodeForArea(activeDamageArea);
            if (isInDamageArea && Input.GetKeyDown(correctKey) && !hasDamaged)
            {
                fishHP -= damagePerSecond;
                fishHP = Mathf.Max(fishHP, 0);
                if (fishHPSlider != null)
                {
                    fishHPSlider.value = fishHP;
                }

                hasDamaged = true;
                damageAreaImage.gameObject.SetActive(false);
                StartCoroutine(ChangeNeedleColorBriefly());
                StartCoroutine(ReactivateDamageAreaAfterDelay());
            }
            else
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
            }
        }

        if (!isInDamageArea)
        {
            hasDamaged = false;
        }
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

private void ActivateRandomDamageArea()
{
    activeDamageArea = UnityEngine.Random.Range(0, 4); // 0: Kanan, 1: Atas, 2: Kiri, 3: Bawah
    damageAreaImage.gameObject.SetActive(true);
    damageAreaImage.fillAmount = 0.25f; // Set fill amount to 0.25
    // Mengubah rotasi area damage berdasarkan area yang aktif
    var area = GetAreaAngles(activeDamageArea);
    damageAreaImage.transform.rotation = Quaternion.Euler(0, 0, area.startAngle);
}

    private (float startAngle, float endAngle) GetAreaAngles(int index)
    {
        return index switch
        {
            0 => (315f, 45f),
            1 => (45f, 135f),
            2 => (135f, 225f),
            3 => (225f, 315f),
            _ => (0f, 0f),
        };
    }

    private KeyCode GetKeyCodeForArea(int index)
    {
        return index switch
        {
            0 => KeyCode.RightArrow,
            1 => KeyCode.UpArrow,
            2 => KeyCode.LeftArrow,
            3 => KeyCode.DownArrow,
            _ => KeyCode.None,
        };
    }

    private IEnumerator ReactivateDamageAreaAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeReactivatingDamageArea);
        ActivateRandomDamageArea();
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