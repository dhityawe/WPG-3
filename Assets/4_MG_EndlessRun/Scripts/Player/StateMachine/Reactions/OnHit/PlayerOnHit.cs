using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the player is hit by an obstacle
        if (other.CompareTag("Obstacle"))
        {
            // Use the singleton instance to load the MG_Reeling scene
            SceneLoader.Instance._MGReeling();
        }

        if (other.CompareTag("Enemy"))
        {
            // Use the singleton instance to load the MG_Reeling scene
            SceneLoader.Instance._MGReeling();
        }
    }
}
