using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPooling : MonoBehaviour
{
    public GameObject[] pathPrefabs; // Array of path prefabs
    public int poolSize = 5; // Size of the path pool
    public float recycleOffset = -50f; // Z-position at which the path gets recycled
    public float pathSpeed = 5f; // Speed at which the paths move
    public GameObject pathParent; // Reference to the PathParent GameObject
    public GameObject pathSpawner; // Reference to the path spawner GameObject

    private Queue<GameObject> pathPool = new Queue<GameObject>(); // Queue to store pooled paths

    void Start()
    {
        // Create the path pool
        CreatePathPool();

        // Initialize paths
        for (int i = 0; i < poolSize; i++)
        {
            ReusePath();
        }
    }

    void Update()
    {
        MovePaths();
    }

    void CreatePathPool()
    {
        // Instantiate path prefabs and add them to the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject path = Instantiate(pathPrefabs[Random.Range(0, pathPrefabs.Length)]);
            path.SetActive(false); // Deactivate initially
            path.transform.parent = pathParent.transform; // Set the parent
            pathPool.Enqueue(path);
        }
    }

    void MovePaths()
    {
        // Move the PathParent instead of each individual path
        pathParent.transform.Translate(Vector3.back * pathSpeed * Time.deltaTime);
        
        foreach (GameObject path in pathPool)
        {
            if (path.activeInHierarchy)
            {
                // Check if the path needs recycling
                if (path.transform.position.z < recycleOffset)
                {
                    ReusePath();
                }
            }
        }
    }

    void ReusePath()
    {
        // Reuse a path from the pool
        GameObject pathToReuse = pathPool.Dequeue();
        pathToReuse.SetActive(true); // Activate the path

        // Reposition path at the pathSpawner position
        pathToReuse.transform.position = pathSpawner.transform.position; // Set position to pathSpawner
        pathToReuse.transform.localPosition = new Vector3(0, 0, 0); // Adjust local position if necessary

        // Add the reused path back into the pool
        pathPool.Enqueue(pathToReuse);
    }

    float CalculateNextSpawnPosition()
    {
        // Find the last active path's Z position relative to PathParent
        float maxZ = 0f;
        foreach (GameObject path in pathPool)
        {
            if (path.activeInHierarchy && path.transform.localPosition.z > maxZ)
            {
                maxZ = path.transform.localPosition.z;
            }
        }
        float newPathLength = pathPool.Peek().GetComponent<Renderer>().bounds.size.z;
        return maxZ + newPathLength;
    }
}
