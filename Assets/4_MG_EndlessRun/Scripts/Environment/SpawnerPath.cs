using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPath : MonoBehaviour
{
    public GameObject[] objectSpawnToPool; // Array of objects to spawn

    [Header("Object Pooling")]
    public GameObject pathPoolParent; // Reference to the object pool parent
    public int poolSize = 10; // Size of the object pool
    private List<GameObject> pathPool; // List to store the pooled objects

    [Header("Spawn Settings")]
    public float spawnInterval = 4.0f; // Interval between object spawns
    public GameObject pathParent; // Parent GameObject for the spawned objects

    private void Start()
    {
        // Initialize object pool
        CreateObjectPool();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the correct tag
        {
            SpawnObject();
        }
    }

    private void CreateObjectPool()
    {
        pathPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject pooledObject = InstantiateRandomObject();
            pooledObject.SetActive(false); // Deactivate object initially
            pathPool.Add(pooledObject);  // Add to pool
        }
    }

    private GameObject InstantiateRandomObject()
    {
        GameObject objectToSpawn = objectSpawnToPool[Random.Range(0, objectSpawnToPool.Length)];
        return Instantiate(objectToSpawn, pathPoolParent.transform);
    }

    private GameObject GetPooledObject()
    {
        foreach (GameObject obj in pathPool)
        {
            if (!obj.activeInHierarchy) // Check for inactive object
            {
                return obj;
            }
        }
        return null; // No available objects
    }

    public void SpawnObject()
    {
        GameObject obj = GetPooledObject();

        if (obj != null)
        {
            // Set the object's position to the spawner's current position
            obj.transform.position = transform.position;

            // Set the object's parent to pathParent (using the transform of the GameObject)
            obj.transform.SetParent(pathParent.transform, true);

            // Activate the object
            obj.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No available objects in the pool to spawn.");
        }
    }
} 
