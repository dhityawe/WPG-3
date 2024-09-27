using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPooling : MonoBehaviour
{
    public float prefabBulletAmount;
    public GameObject bulletPrefab;
    public GameObject bulletPoolParent;
    public PlayerBase playerBase;

    void Start()
    {   
        playerBase = GetComponent<PlayerBase>();
        InitializePoolingData();
        InstantiateBulletPool();
    }
    void InitializePoolingData()
    {
        prefabBulletAmount = playerBase.ammoCapacity;
        bulletPrefab = playerBase.bulletPrefab;
        bulletPoolParent = playerBase.bulletPool;
    }


    void InstantiateBulletPool()
    {
        for (int i = 0; i < prefabBulletAmount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPoolParent.transform);
            bullet.SetActive(false);
        }
    }

    

    
}
