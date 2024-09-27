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
    public float bulletPullDistance;
    public float bulletSpeed;
    public float bulletAmmo;
    public float ammoCapacity;
    public float shootingBulletCd;
    public float reloadBulletCd;

    [Header("Stats HookShot")]
    public bool isAnimationRunning;
    public bool isHookShotAble;
    public float reloadHookCd;

    [Header("Player Object Reference")]
    public GameObject currentHookObject;

    [Header("Bullet Object Reference")]
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public GameObject bulletPool;

    void Start()
    {
        InitializeGameObject();
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
        bulletPullDistance = fishingRodBase.bulletPullDistance;
        bulletSpeed = fishingRodBase.bulletSpeed;
        bulletAmmo = fishingRodBase.bulletAmmo;
        ammoCapacity = fishingRodBase.ammoCapacity;
        shootingBulletCd = fishingRodBase.shootingBulletCd;
        reloadBulletCd = fishingRodBase.reloadBulletCd;
        isHookShotAble = fishingRodBase.isHookShotAble;
        reloadHookCd = fishingRodBase.reloadHookCd;
        bulletPrefab = fishingRodBase.bulletPrefab;
    }

    public void InitializeGameObject()
    {
        currentHookObject = this.gameObject;
        bulletSpawnPoint = GameObject.Find("BulletSpawnPoint").transform;
        bulletPool = GameObject.Find("BulletPool");
    }
    public void IntializeAmmoCapacity()
    {
        ammoCapacity = bulletAmmo;
    }
}
