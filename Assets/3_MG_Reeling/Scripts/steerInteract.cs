using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SteerInteract : MonoBehaviour
{
    public void Interact()
    {
        // Logika interaksi
        Debug.Log("Objek diinteraksi: " + gameObject.name);
        // Pindah scene
        SceneManager.LoadScene("2 2D_Explore");

    }
}