using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishDatabase", menuName = "ScriptableObjects/FishDatabase", order = 2)]
public class FishDatabase : ScriptableObject
{
    [Header("Common Fish")]
    public List<FishBase> commonFish;

    [Header("Uncommon Fish")]
    public List<FishBase> uncommonFish;

    [Header("Anomaly Fish")]
    public List<FishBase> anomalyFish;

    [Header("Boss Fish")]
    public List<FishBase> bossFish;

    // A utility function to get all fish as a single list
    public List<FishBase> GetAllFish()
    {
        List<FishBase> allFish = new List<FishBase>();
        allFish.AddRange(commonFish);
        allFish.AddRange(uncommonFish);
        allFish.AddRange(anomalyFish);
        allFish.AddRange(bossFish);
        return allFish;
    }
}
