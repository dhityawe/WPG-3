using UnityEngine;

public class Obstacle : MonoBehaviour, IDestroyable
{
    public Transform obstaclePoolParent; // Reference to the ObstaclePool parent object

    public void DeactivateObject()
    {
        gameObject.SetActive(false); // Deactivate the obstacle
        transform.SetParent(obstaclePoolParent); // Move it back to the pool
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            DeactivateObject();
        }
    }
}
