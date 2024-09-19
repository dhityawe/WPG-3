using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 10.0f;  // Increased speed for faster transition
    public float xBounds = 2.0f;     // Boundaries for X-axis (-2 to 2)
    public float minY = 1.0f;        // Minimum Y boundary (1)
    public float maxY = 2.0f;        // Maximum Y boundary (2)
    public float laneDistance = 2.0f; // Distance between lanes (horizontal and vertical)

    public FishBase fishBase; // Reference to the FishBase script
}
