using UnityEngine;
using System.Collections;

public class HookShotState : IPlayerState
{
    private float hookShotDistance = 5.0f; // Distance to move the hook on the Z axis
    private float hookShotAnimationTime = 2.0f;    // Speed of the hook shot movement
    private GameObject currentHookObject;
    private PlayerBase playerBase; // Reference to the PlayerBase component

    private Coroutine hookShotCoroutine; // To keep track of the coroutine

    public void EnterState(PlayerStateManager player)
    {
        playerBase = player.GetComponent<PlayerBase>();
        currentHookObject = playerBase.currentHookObject;
        Debug.Log("Entered Hook Shot State");
    }

    public void UpdateState(PlayerStateManager player)
    {
        if (Input.GetKeyDown(KeyCode.B)) // Switch to BulletShoot state
        {
            player.SwitchState(player.bulletShootState);
        }

        if (!playerBase.isAnimationRunning && playerBase.isHookShotAble && Input.GetKeyDown(KeyCode.Space))
        {
            // Stop any existing coroutines (if necessary)
            if (hookShotCoroutine != null)
            {
                playerBase.StopCoroutine(hookShotCoroutine);
            }

            // Disable hook shot ability and start the hook shot coroutine
            playerBase.isHookShotAble = false;
            playerBase.isAnimationRunning = true;

            hookShotCoroutine = player.StartCoroutine(SmoothMoveHook(player));  // Store the coroutine
            Debug.Log("Hook Shot Fired, isHookShotAble: " + playerBase.isHookShotAble);
        }
        else if (playerBase.isAnimationRunning && !playerBase.isHookShotAble)
        {
            Debug.Log("Still HookShot animation, can't move");
        }
    }

    public void ExitState(PlayerStateManager player)
    {
        // Stop any running coroutine when exiting the state to avoid overlap
        if (hookShotCoroutine != null)
        {
            playerBase.StopCoroutine(hookShotCoroutine);
        }
        Debug.Log("Exiting Hook Shot State");
    }

    // Coroutine for moving the hook forward and then returning it
    private IEnumerator SmoothMoveHook(PlayerStateManager player)
    {
        Vector3 startPosition = currentHookObject.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + hookShotDistance);

        // Move forward
        yield return playerBase.StartCoroutine(SmoothMove(currentHookObject.transform, targetPosition));

        // Wait for hook to stay extended
        yield return new WaitForSeconds(hookShotAnimationTime);

        // Move back
        yield return playerBase.StartCoroutine(SmoothMove(currentHookObject.transform, startPosition));

        playerBase.isAnimationRunning = false; // Reset animation flag
        Debug.Log("Hook Shot Animation Complete, isAnimationRunning: " + playerBase.isAnimationRunning);

        player.SwitchState(player.reloadState); // Switch to reload state
    }

    // Coroutine to smoothly move an object to a target position
    private IEnumerator SmoothMove(Transform objTransform, Vector3 targetPosition)
    {
        float elapsedTime = 0;
        float totalTime = 0.3f; // Adjust total time for movement

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
