using UnityEngine;

public class Collectible : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        // Add the obstacle to the player's ammo or inventory
        // PlayerAmmoManager.Instance.AddAmmo(1); // Example method to add ammo
        gameObject.SetActive(false); // Deactivate the object after collection
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player triggers the collection
        {
            Collect();
        }
    }
}
