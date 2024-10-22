using UnityEngine;

namespace MG_Reeling {
    public static class PositionRotationManager
    {
        public static void SavePlayerPositionAndRotation(GameObject player, Camera camera)
        {
            if (player != null)
            {
                Vector3 playerPosition = player.transform.position;
                PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
                PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
                PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);

                // Save player rotation
                float playerRotY = player.transform.localRotation.eulerAngles.y;
                PlayerPrefs.SetFloat("PlayerRotY", playerRotY);
            }

            if (camera != null)
            {
                Vector3 cameraPosition = camera.transform.position;
                Quaternion cameraRotation = camera.transform.rotation;
                PlayerPrefs.SetFloat("CameraX", cameraPosition.x);
                PlayerPrefs.SetFloat("CameraY", cameraPosition.y);
                PlayerPrefs.SetFloat("CameraZ", cameraPosition.z);
                PlayerPrefs.SetFloat("CameraRotX", cameraRotation.x);
                PlayerPrefs.SetFloat("CameraRotY", cameraRotation.y);
                PlayerPrefs.SetFloat("CameraRotZ", cameraRotation.z);
                PlayerPrefs.SetFloat("CameraRotW", cameraRotation.w);
            }

            PlayerPrefs.Save();
        }

        public static void LoadPlayerPositionAndRotation(GameObject player, Camera camera)
        {
            // Load player position if it exists
            if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
            {
                float x = PlayerPrefs.GetFloat("PlayerX");
                float y = PlayerPrefs.GetFloat("PlayerY");
                float z = PlayerPrefs.GetFloat("PlayerZ");
                if (player != null)
                {
                    player.transform.position = new Vector3(x, y, z);
                }
            }

            // Load camera position and rotation if it exists
            if (PlayerPrefs.HasKey("CameraX") && PlayerPrefs.HasKey("CameraY") && PlayerPrefs.HasKey("CameraZ") &&
                PlayerPrefs.HasKey("CameraRotX") && PlayerPrefs.HasKey("CameraRotY") && PlayerPrefs.HasKey("CameraRotZ") && PlayerPrefs.HasKey("CameraRotW"))
            {
                float camX = PlayerPrefs.GetFloat("CameraX");
                float camY = PlayerPrefs.GetFloat("CameraY");
                float camZ = PlayerPrefs.GetFloat("CameraZ");
                float camRotX = PlayerPrefs.GetFloat("CameraRotX");
                float camRotY = PlayerPrefs.GetFloat("CameraRotY");
                float camRotZ = PlayerPrefs.GetFloat("CameraRotZ");
                float camRotW = PlayerPrefs.GetFloat("CameraRotW");
                if (camera != null)
                {
                    camera.transform.SetPositionAndRotation(
                        new Vector3(camX, camY, camZ),
                        new Quaternion(camRotX, camRotY, camRotZ, camRotW)
                    );
                }
            }

            // Load player rotation if it exists
            if (PlayerPrefs.HasKey("PlayerRotY"))
            {
                float playerRotY = PlayerPrefs.GetFloat("PlayerRotY");
                if (player != null)
                {
                    player.transform.localRotation = Quaternion.Euler(0.0f, playerRotY, 0.0f);
                }
            }
        }
    }
}