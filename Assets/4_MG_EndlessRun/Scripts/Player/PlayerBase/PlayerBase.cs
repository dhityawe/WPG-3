using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [Header("Stats General")]
    public string rodName;
    public Rarity rarity;
    public string description;
    public float price;
    public Sprite sprite;

    [Header("Stats Endless Runner (Hook)")]

    [Header("Stats Movement")]
    public float MoveSpeed;

    [Header("Stats Shooting")]
    public float bulletPullPower;
    public float projectileSpeed;
    public float hookShotCd;
    public float ReloadCd;

    public FishingRodBase fishingRodBase;

    void Start()
    {
        InitializeStats();
    }

    public void InitializeStats()
    {
        rodName = fishingRodBase.rodName;
        rarity = fishingRodBase.rarity;
        description = fishingRodBase.description;
        price = fishingRodBase.price;
        sprite = fishingRodBase.sprite;
        MoveSpeed = fishingRodBase.MoveSpeed;
        bulletPullPower = fishingRodBase.bulletPullPower;
        projectileSpeed = fishingRodBase.projectileSpeed;
        hookShotCd = fishingRodBase.hookShotCd;
        ReloadCd = fishingRodBase.ReloadCd;
    }
}
