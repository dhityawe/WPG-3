using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MG_Reeling {
    public class FpsPov : MonoBehaviour
    {
        public float mouseSensitivity = 100.0f;
        private float xRotation = 0.0f;
        private Transform playerBody; // Referensi ke objek pemain

        // Start is called before the first frame update
        void Start()
        {
            // Lock the cursor to the center of the screen
            Cursor.lockState = CursorLockMode.Locked;

            // Dapatkan referensi ke objek pemain
            playerBody = transform.parent;

            // Load initial rotation if it exists
            PositionRotationManager.LoadPlayerPositionAndRotation(playerBody.gameObject, GetComponent<Camera>());

            // Extract the xRotation from the loaded rotation
            Quaternion initialRotation = transform.localRotation;
            Vector3 eulerAngles = initialRotation.eulerAngles;
            xRotation = eulerAngles.x;
        }

        // Update is called once per frame
        void Update()
        {
            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Adjust the vertical rotation and clamp it
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

            // Rotate the camera around the X axis
            transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);

            // Rotate the player around the Y axis
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}