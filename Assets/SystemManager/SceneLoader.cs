using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Correct namespace for SceneManager

public class SceneLoader : MonoBehaviour // Renamed class to avoid conflict with Unity's SceneManager
{
    public static SceneLoader Instance { get; private set; } // Singleton instance

    private void Awake()
    {
        // Check if instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }

        Instance = this; // Set instance to this
        DontDestroyOnLoad(gameObject); // Optional: Don't destroy on load if you want to persist
    }

    public void _MainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("0 MainMenu");
    }

    public void _SceneEvent()
    {
        // Load the scene event scene
        SceneManager.LoadScene("1 Scene_Event");
    }

    public void _2DExplore()
    {
        // Load the 2D explore scene
        SceneManager.LoadScene("2 2D_Explore");
    }

    public void _MGReeling()
    {
        // Load the minigame reeling scene
        SceneManager.LoadScene("3 MG_Reeling");
    }

    public void _MGEndlessRun()
    {
        // Load the minigame endless run scene
        SceneManager.LoadScene("4 MG_EndlessRun");
    }

    public void _MGBossFight()
    {
        // Load the minigame boss fight scene
        SceneManager.LoadScene("5 MG_BossFight");
    }

    public void _Seller()
    {
        // Load the seller scene
        SceneManager.LoadScene("6 Seller");
    }

    public void _DayResult()
    {
        // Load the day result scene
        SceneManager.LoadScene("7 Day_Result");
    }
}
