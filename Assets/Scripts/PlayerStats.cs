using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public float maxHealth = 100;
    public float currentHealth = 100;
    public float meleeAttackStat => BuildingCounter.SwordsmenAmount * 5f;
    public float rangedAttackStat => BuildingCounter.SiegeAmount * 10f + BuildingCounter.ArchersAmount * 5;
    public float protectionStat => BuildingCounter.WallAmount * 10f + 100f;
    public int GoldAmount
    {
        get { return goldAmount; }
        set
        {
            goldAmount = value;
            goldText.text = goldAmount.ToString();
        }
    }
    private int goldAmount;
    public TMP_Text goldText;


    public void UpdatePlayerStats(BuildingData buildingData)
    {
        switch (buildingData.buildingType)
        {
            case BuildType.Bank:
                BuildingCounter.BankAmount++;
                Debug.Log($"Banks: {BuildingCounter.BankAmount}");
                Debug.Log($"{PlayerStats.Instance.GoldAmount}");
                break;
            case BuildType.Archers:
                BuildingCounter.ArchersAmount++;
                Debug.Log($"Archers: {BuildingCounter.ArchersAmount}");
                Debug.Log($"{PlayerStats.Instance.rangedAttackStat}");
                break;
            case BuildType.Swordsmen:
                BuildingCounter.SwordsmenAmount++;
                Debug.Log($"Swordsmen: {BuildingCounter.SwordsmenAmount}");
                Debug.Log($"{PlayerStats.Instance.meleeAttackStat}");
                break;
            case BuildType.Siege:
                BuildingCounter.SiegeAmount++;
                Debug.Log($"Siege: {BuildingCounter.SiegeAmount}");
                Debug.Log($"{PlayerStats.Instance.rangedAttackStat}");
                break;
            case BuildType.Wall:
                BuildingCounter.WallAmount++;
                Debug.Log($"Walls: {BuildingCounter.WallAmount}");
                Debug.Log($"{PlayerStats.Instance.protectionStat}");
                break;
        }
    }
    private void RemoveMaterials(BuildingData buildingData)
    {
        MaterialCounter.WoodCounter -= buildingData.buildingWoodRequirement;
        MaterialCounter.RockCounter -= buildingData.buildingRockRequirement;
        MaterialCounter.StringCounter -= buildingData.buildingStringRequirement;
        MaterialCounter.IronCounter -= buildingData.buildingIronRequirement;
        Debug.Log("Are we here?");
    }

    private void OnEnable()
    {
        Actions.OnBuildingBuilt += UpdatePlayerStats;
        Actions.OnBuildingBuilt += RemoveMaterials;
    }

    private void OnDisable()
    {
        Actions.OnBuildingBuilt -= UpdatePlayerStats;
        Actions.OnBuildingBuilt -= RemoveMaterials;
    }
}
