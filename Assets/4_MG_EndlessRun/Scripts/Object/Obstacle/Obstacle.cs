using UnityEngine;

public class Obstacle : MonoBehaviour, IDestroyable
{
    public Transform obstaclePoolParent; // Reference to the ObstaclePool parent object
    
    private SpawnerObject spawnerObject; // Reference to the SpawnerObject script

    private void Start()
    {
        // Find the SpawnerObject script in the scene
        spawnerObject = FindObjectOfType<SpawnerObject>(); 
    }

    public void DeactivateObject()
    {
        // Set the parent to spawnerObject's objectPoolParent before deactivating
        if (spawnerObject != null && spawnerObject.objectPoolParent != null)
        {
            transform.SetParent(spawnerObject.objectPoolParent.transform); // Set the parent to the pool's transform
        }

        gameObject.SetActive(false); // Deactivate the obstacle
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            DeactivateObject(); // Call DeactivateObject if any of the tags match
        }
        if (other.CompareTag("Player"))
        {
            DeactivateObject(); // Call DeactivateObject if any of the tags match
        }
        if (other.CompareTag("PlayerBullet"))
        {
            DeactivateObject(); // Call DeactivateObject if any of the tags match
        }
    }
}
