using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics.Contracts;

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

    public int maxHealth => 10000;
    public int currentHealth = 10000;
    public int meleeAttackStat => BuildingCounter.SwordsmenAmount * 5;
    public int siegeAttackStat => BuildingCounter.SiegeAmount * 10;   
    public int archersAttackStat =>BuildingCounter.ArchersAmount * 5;
    public int protectionStat => BuildingCounter.WallAmount * 10;


    public void UpdatePlayerStats(BuildingData buildingData)
    {
        switch (buildingData.buildingType)
        {
            case BuildType.Bank:
                BuildingCounter.BankAmount++;
                break;
            case BuildType.Archers:
                BuildingCounter.ArchersAmount++;
                break;
            case BuildType.Swordsmen:
                BuildingCounter.SwordsmenAmount++;
                break;
            case BuildType.Siege:
                BuildingCounter.SiegeAmount++;
                break;
            case BuildType.Wall:
                BuildingCounter.WallAmount++;
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
