using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [Header("Fishing Rod Stats Base")]
    public FishingRodBase fishingRodBase;

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
    public GameObject currentHookObject;
    public GameObject bulletPrefab;

    void Start()
    {
        // Get the current GameObject's reference
        currentHookObject = this.gameObject;

        InitializePlayerBase();
    }
    
    public void InitializePlayerBase()
    {
        InitializeStats();
        IntializeAmmoCapacity();
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
        isHookShotAble = fishingRodBase.isHookShotAble;
        ReloadHookCd = fishingRodBase.ReloadHookCd;
        bulletPrefab = fishingRodBase.bulletPrefab;
    }

    public void IntializeAmmoCapacity()
    {
        ammoCapacity = bulletAmmo;
    }
}
