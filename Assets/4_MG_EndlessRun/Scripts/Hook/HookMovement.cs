using UnityEngine;

public class HookMovement : MonoBehaviour
{
    public float currentPosX;
    public float currentPosY;
    public float manouverSpeed; // Speed of the hook

    // Reference to other scripts
    public PathManager pathManager;

    // Start is called before the first frame update
    
    void Update()
    {
        // get the current transform position of this gameObject
        currentPosX = transform.position.x;
        currentPosY = transform.position.y;

        


    }
    
    public void MovementInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
        
    }
    else if (Input.GetKeyDown(KeyCode.RightArrow))
    {
        
    }
    
    // Handle up/down arrow input for vertical movement
    if (Input.GetKeyDown(KeyCode.UpArrow))
    {
       
    }
    else if (Input.GetKeyDown(KeyCode.DownArrow))
    {
        
    }
    }
}
