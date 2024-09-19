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


    public FishBase fishBase; // Reference to the FishBase script

    private void Awake()
    {
        DataInitialisation();
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
