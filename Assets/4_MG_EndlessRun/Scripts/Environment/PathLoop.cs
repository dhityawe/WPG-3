using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLoop : MonoBehaviour
{
    public GameObject pathSpawner; // GameObject indicating where the path should be spawned
    public GameObject pathPoolParent; // GameObject parent holding all the paths as children
    public float movementSpeed = 5f; // Speed at which this path moves towards the player
    public string playerTag = "Player"; // Tag for the player object

    private List<GameObject> pathList; // List to hold references to all paths

    // Start is called before the first frame update
    void Start()
    {
        // Initialize path list with all the children of pathPoolParent
        pathList = new List<GameObject>();

        foreach (Transform child in pathPoolParent.transform)
        {
            pathList.Add(child.gameObject); // Add each child (path) to the list
        }

        // Optionally, set the first path in the list as the current path to move initially
        SpawnRandomPath();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the GameObject that this script is attached to
        MovePath();
    }

    // This method moves this GameObject (attached to this script) towards the player along the -z axis
    void MovePath()
    {
        transform.Translate(Vector3.back * movementSpeed * Time.deltaTime); // Move along -z axis
    }

    // When the collider enters the trigger area, check if it is the player
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that triggered is the player
        if (other.CompareTag("Player")) // Ensure the player has the correct tag
        {
            Debug.Log("Player has entered the trigger!");
            // Move a random path to the spawner position
            SpawnRandomPath();

            // Optionally, deactivate this path and return it to the pool
            gameObject.SetActive(false);
        }
    }

    // Randomly selects a path from the pool and moves it to the spawner position
    void SpawnRandomPath()
    {
        if (pathList.Count == 0)
        {
            Debug.LogWarning("Path list is empty! Cannot spawn a path.");
            return; // Prevent further execution if no paths are available
        }

        // Randomly pick a path from the list
        int randomIndex = Random.Range(0, pathList.Count);
        GameObject currentPath = pathList[randomIndex];

        // Set the position of the chosen path to the spawner's position
        currentPath.transform.position = pathSpawner.transform.position;

        // Activate the new path
        currentPath.SetActive(true);
    }
}
