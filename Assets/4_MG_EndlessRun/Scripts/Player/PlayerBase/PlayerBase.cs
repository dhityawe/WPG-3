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

    [Header("Stats BulletShoot")]
    // public float bulletPullPower;
    public float bulletSpeed;
    public float bulletAmmo;
    public float ReloadBulletCd;

    [Header("Stats HookShot")]
    public float hookShotCd;
    public float hookShotAmmo;
    public float ReloadHookCd;

    [Header("Stats Reference")]
    public GameObject bulletPrefab;
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
        bulletSpeed = fishingRodBase.bulletSpeed;
        bulletAmmo = fishingRodBase.bulletAmmo;
        ReloadBulletCd = fishingRodBase.ReloadBulletCd;
        hookShotCd = fishingRodBase.hookShotCd;
        hookShotAmmo = fishingRodBase.hookShotAmmo;
        ReloadHookCd = fishingRodBase.ReloadHookCd;
    }
}
