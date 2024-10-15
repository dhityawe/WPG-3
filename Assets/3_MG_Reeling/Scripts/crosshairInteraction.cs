using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairInteraction : MonoBehaviour
{
    public float interactionDistance = 5.0f;
    public LayerMask interactableLayer;
    private Renderer lastRenderer;
    private Color originalColor;
    private bool colorChanged = false;

    void Update()
    {
        // Buat ray dari tengah layar
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Cek apakah ray mengenai objek dalam jarak interaksi
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            // Panggil metode interaksi pada objek yang ditunjuk
            SteerInteract interactable = hit.collider.GetComponent<SteerInteract>();
            if (interactable != null)
            {
                Renderer renderer = hit.collider.GetComponent<Renderer>();
                if (renderer != null)
                {
                    // Jika objek yang ditunjuk berbeda dari objek sebelumnya, kembalikan warna objek sebelumnya
                    if (lastRenderer != null && lastRenderer != renderer)
                    {
                        lastRenderer.material.color = originalColor;
                        colorChanged = false;
                    }

                    // Simpan referensi ke objek yang ditunjuk dan warnanya
                    if (!colorChanged)
                    {
                        lastRenderer = renderer;
                        originalColor = renderer.material.color;
                        colorChanged = true;
                    }

                    // Ubah warna objek menjadi lebih gelap
                    renderer.material.color = originalColor * 0.9f;
                }

                // Cek apakah tombol interaksi ditekan
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }
        else
        {
            // Kembalikan warna objek terakhir jika tidak ada objek yang ditunjuk
            if (lastRenderer != null && colorChanged)
            {
                lastRenderer.material.color = originalColor;
                lastRenderer = null;
                colorChanged = false;
            }
        }
    }
}