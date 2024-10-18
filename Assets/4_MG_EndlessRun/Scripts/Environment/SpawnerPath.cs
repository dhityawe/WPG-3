using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPath : MonoBehaviour
{
    public GameObject[] pathSpawnToPool; // Array of objects to spawn

    [Header("Object Pooling")]
    public GameObject pathPoolParent; // Reference to the object pool parent
    public int poolSize = 10; // Size of the object pool
    private List<GameObject> pathPool; // List to store the pooled objects

    private void Start()
    {
        // Initialize object pool
        CreatePathPool();
    }

    void Update()
    {
        // Check if the first child of this object has a z position <= -10
        if (transform.GetChild(0).position.z <= 0)
        {
            // Move the first child back to the pool and deactivate it
            GameObject firstChild = transform.GetChild(0).gameObject;
            firstChild.transform.SetParent(pathPoolParent.transform);
            firstChild.SetActive(false);

            // Spawn a new path
            SpawnPath();
        }
    }

    private void CreatePathPool()
    {
        pathPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject pooledObject = InstantiateRandomPath();
            pooledObject.SetActive(false); // Deactivate object initially
            pathPool.Add(pooledObject); // Add to pool
        }
    }

    private GameObject InstantiateRandomPath()
    {
        GameObject objectToSpawn = pathSpawnToPool[Random.Range(0, pathSpawnToPool.Length)];
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

    public void SpawnPath()
    {
        GameObject obj = GetPooledObject();

        if (obj != null)
        {
            // Set the object's parent to this object
            obj.transform.SetParent(transform);

            // Move the object to the last child index position
            obj.transform.SetSiblingIndex(transform.childCount - 1);

            // Set the object's position
            obj.transform.position = new Vector3(0, 0f, 372f);

            // Activate the object
            obj.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No available objects in the pool to spawn.");
        }
    }
}
