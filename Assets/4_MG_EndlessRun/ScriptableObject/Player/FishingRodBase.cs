using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishingRodBase", menuName = "PlayerBase/FishingRodBase", order = 1)]
public class FishingRodBase : ScriptableObject
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

    [Header("Stats BulletShoot")]
    // public float bulletPullPower;
    public float bulletSpeed;
    public float bulletAmmo;
    public float ammoCapacity;
    public float ShootingBulletCd;
    public float ReloadBulletCd;

    [Header("Stats HookShot")]
    public bool isAnimationRunning;
    public bool isHookShotAble;
    public float ReloadHookCd;

    [Header("Stats Reference")]
    public GameObject bulletPrefab;
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
