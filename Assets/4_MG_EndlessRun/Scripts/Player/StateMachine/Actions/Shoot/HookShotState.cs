using UnityEngine;
using System.Collections;

public class HookShotState : IPlayerState
{
    private float hookShotDistance = 5.0f; // Distance to move the hook on the Z axis
    private float hookShotAnimationTime = 2.0f;    // Speed of the hook shot movement
    private GameObject currentHookObject;
    private PlayerBase playerBase; // Reference to the PlayerBase component

    public void EnterState(PlayerStateManager player)
    {
        // Find the PlayerBase component on the player GameObject
        playerBase = player.GetComponent<PlayerBase>();

        // Get the current Hook Object
        currentHookObject = playerBase.currentHookObject;

        Debug.Log("Entered Hook Shot State");
    }

    public void UpdateState(PlayerStateManager player)
    {
        // Handle hook shot logic here
        if (Input.GetKeyDown(KeyCode.B)) // Example: Switch to BulletShoot state
        {
            player.SwitchState(player.bulletShootState);
        }

        if (playerBase.isAnimationRunning == false && playerBase.isHookShotAble && Input.GetKeyDown(KeyCode.Space)) // Input for shooting the hook
        {
            // Disable hook shot ability and start the hook shot coroutine
            playerBase.isAnimationRunning = true;
            playerBase.isHookShotAble = false;
            player.StartCoroutine(SmoothMoveHook(player));
            Debug.Log("Hook Shot Fired");
        }
        else if (playerBase.isAnimationRunning == true && !playerBase.isHookShotAble)
        {
            Debug.Log("Still HookShot animation, can't move");
        }
    }

    public void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exiting Hook Shot State");
    }

    // Coroutine for moving the hook forward and then returning it
    IEnumerator SmoothMoveHook(PlayerStateManager player)
    {
        Vector3 startPosition = currentHookObject.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + hookShotDistance);

        // Move forward
        yield return playerBase.StartCoroutine(SmoothMove(currentHookObject.transform, targetPosition));

        // Wait for hook to stay extended (hookShotAnimationTime)
        yield return new WaitForSeconds(hookShotAnimationTime);

        // Move back
        yield return playerBase.StartCoroutine(SmoothMove(currentHookObject.transform, startPosition));

        playerBase.isAnimationRunning = false; // Reset the animation flag
        Debug.Log("Hook Shot Animation Complete");

        player.SwitchState(player.reloadState); // Switch to reload state
    }

    // Coroutine to smoothly move an object to a target position
    IEnumerator SmoothMove(Transform objTransform, Vector3 targetPosition)
    {
        float elapsedTime = 0;
        float totalTime = 0.3f; // Adjust the total time it takes to complete the movement (can be based on speed)

        Vector3 startPosition = objTransform.position;

        while (elapsedTime < totalTime)
        {
            objTransform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / totalTime));
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the object ends up exactly at the target position
        objTransform.position = targetPosition;
    }
}
