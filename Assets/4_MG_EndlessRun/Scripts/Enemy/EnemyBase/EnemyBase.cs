using System.Collections;
using UnityEditor.Sprites;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public string enemyName;
    public string rarity;
    public int price;
    public Sprite sprite;

    [Header("Enemy Stats")]

    [Header("Stats Health")]
    public float enemyHealth;

    [Header("Stats Movement")]
    public float enemyMoveSpeed;
    public float enemyMoveCd;

    [Header("Stats Attack")]
    public float attackKnockback;
    public float attackInterval;

    [Header("Reference")]
    public Transform enemyPosition; // Reference to the enemy position
    public GameObject enemyBulletPrefab; // Reference to the enemy bullet prefab
    public FishBase fishBase; // Reference to the FishBase script

    private void Awake()
    {
        DataInitialisation();
    }

    public void Update()
    {
        // get the enemy position from this gameObject
        enemyPosition = this.transform;

    }

    public void ShootCooldown()
    {
        StartCoroutine(ReloadEnemyBullet());
    }

    private IEnumerator ReloadEnemyBullet()
    {
        yield return new WaitForSeconds(attackInterval);
    }
    private void DataInitialisation()
    {
        enemyName = fishBase.fishName;
        rarity = fishBase.rarity.ToString();
        price = fishBase.price;
        sprite = fishBase.sprite;

        enemyHealth = fishBase.enemyHealth;
        enemyMoveSpeed = fishBase.enemyMoveSpeed;
        enemyMoveCd = fishBase.enemyMoveCd;
        attackKnockback = fishBase.attackKnockback;
        attackInterval = fishBase.attackInterval;
    }
}
