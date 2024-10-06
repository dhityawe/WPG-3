using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPooling : MonoBehaviour
{
    public float prefabBulletAmount; // Changed to int to match the pool size requirement
    public GameObject bulletPrefab; // Bullet prefab
    public GameObject bulletPoolParent; // Parent object for the bullet pool
    public PlayerBase playerBase; // Reference to PlayerBase

    void Start()
    {   

        // Check if playerBase is null and log a warning if so
        if (playerBase == null)
        {
            Debug.LogError("PlayerBase reference is not set! Make sure it is attached to the same GameObject.");
            return; // Exit early to prevent further errors
        }

        // Initialize pooling data
        InitializePoolingData();
        
        // Instantiate the bullet pool
        InstantiateBulletPool();
    }

    void InitializePoolingData()
    {
        // Validate ammo capacity
        if (playerBase.ammoCapacity <= 0)
        {
            Debug.LogWarning("Ammo capacity must be greater than zero.");
            return; // Prevent further processing if ammo capacity is invalid
        }

        prefabBulletAmount = playerBase.ammoCapacity; // Get ammo capacity
        bulletPrefab = playerBase.bulletPrefab; // Get bullet prefab

        // Check if bullet prefab is assigned
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab is not assigned in PlayerBase. Please assign it.");
            return; // Exit early to prevent further errors
        }

        bulletPoolParent = playerBase.bulletPool; // Get the bullet pool parent

        // Check if bullet pool parent is assigned
        if (bulletPoolParent == null)
        {
            Debug.LogError("Bullet pool parent is not assigned in PlayerBase. Please assign it.");
            return; // Exit early to prevent further errors
        }
    }

    void InstantiateBulletPool()
    {
        // Instantiate the bullet pool based on the prefabBulletAmount
        for (int i = 0; i < prefabBulletAmount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPoolParent.transform);
            bullet.SetActive(false); // Deactivate the bullet initially
        }
    }
}
