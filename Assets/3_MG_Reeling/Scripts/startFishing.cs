using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startFishing : MonoBehaviour
{
    private bool isPlayerInCollider = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInCollider && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("4 MG_ShootEmUp"); // Ganti "FishingScene" dengan nama scene yang ingin dipindahkan
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