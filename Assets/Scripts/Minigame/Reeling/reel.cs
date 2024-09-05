using UnityEngine;
using UnityEngine.UI;

public class reel : MonoBehaviour
{
    public GameObject jarum;
    public float rotationSpeed = 100f;
    public float fishHP = 100f;
    public float maxFishHP = 100f; // Maximum HP of the fish
    public float damageAreaStartAngleKanan = 315f;
    public float damageAreaEndAngleKanan = 45f;
    public float damageAreaStartAngleKiri = 135f;
    public float damageAreaEndAngleKiri = 225f;
    public float damagePerSecond = 10f;
    public Slider fishHPSlider; // Reference to the UI Slider element
    private bool damageGiven = false;
    private Image jarumImage;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Image component from the jarum GameObject
        jarumImage = jarum.GetComponent<Image>();

        // If the Image component is not found, log an error
        if (jarumImage == null)
        {
            Debug.LogError("No Image component found on the jarum GameObject.");
        }

        // Initialize the fish HP slider
        if (fishHPSlider != null)
        {
            fishHPSlider.maxValue = maxFishHP;
            fishHPSlider.value = fishHP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the needle
        jarum.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Get the current rotation angle of the needle
        float currentAngle = jarum.transform.eulerAngles.z;

        // Check if the needle is within the damage areas
        bool isInTopDamageArea = (currentAngle >= damageAreaStartAngleKanan || currentAngle <= damageAreaEndAngleKanan);
        bool isInBottomDamageArea = (currentAngle >= damageAreaStartAngleKiri && currentAngle <= damageAreaEndAngleKiri);

        // Check for user input to give damage
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if ((isInTopDamageArea || isInBottomDamageArea) && !damageGiven)
            {
                // Reduce fish HP
                fishHP -= damagePerSecond;
                fishHP = Mathf.Max(fishHP, 0); // Ensure HP doesn't go below 0

                // Update the fish HP slider
                if (fishHPSlider != null)
                {
                    fishHPSlider.value = fishHP;
                }

                // Provide visual feedback (e.g., change color of the needle)
                if (jarumImage != null)
                {
                    jarumImage.color = Color.red;
                }

                // Set damageGiven to true to prevent multiple damages
                damageGiven = true;
            }
        }

        // Reset damageGiven when the key is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            damageGiven = false;

            // Reset visual feedback
            if (jarumImage != null)
            {
                jarumImage.color = Color.white;
            }
        }
    }
}