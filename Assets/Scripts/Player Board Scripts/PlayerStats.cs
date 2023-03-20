using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics.Contracts;
using Photon.Pun;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        playerProperties["MaxHealth"] = 10000;
        playerProperties["CurrentHealth"] = 10000;
        UpdatePlayerProperties();
        Debug.Log(playerProperties["CurrentHealth"]);
    }

    public ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    public int maxHealth = 10000;
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
        UpdatePlayerProperties();

    }
    private void RemoveMaterials(BuildingData buildingData)
    {
        MaterialCounter.WoodCounter -= buildingData.buildingWoodRequirement;
        MaterialCounter.RockCounter -= buildingData.buildingRockRequirement;
        MaterialCounter.StringCounter -= buildingData.buildingStringRequirement;
        MaterialCounter.IronCounter -= buildingData.buildingIronRequirement;
    }

    public void UpdatePlayerProperties()
    {
        
        playerProperties["MeleeStat"] = meleeAttackStat;
        playerProperties["SiegeStat"] = siegeAttackStat;
        playerProperties["ArchersStat"] = archersAttackStat;
        playerProperties["DefenseStat"] = protectionStat;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
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
