using UnityEngine;

[CreateAssetMenu(fileName = "FishBase", menuName = "ScriptableObjects/FishBase", order = 1)]
public class FishBase : ScriptableObject
{
    public string fishName;
    public Rarity rarity;
    public int price;
    public Sprite sprite;

    [Header("Fish Enemy Stats")] // This should be applied to a field, not an enum

    [Header("Stats")]
    public int enemyHealth;  // Example of a field where you could use the header
    
    [Header("Stats Movement")]
    public int enemyMoveSpeed;
    public int enemyMoveCd;
    
    [Header("Stats Attack")]
    public int attackDamage;
    public int attackInterval;

    public enum Rarity
    {
        Common,
        Uncommon,
        Anomaly,
        Boss
    }


}
