using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public void Build(BuildingData buildingData)
    {
        if (!PlayerHasEnoughMaterials(buildingData))
        {
            Debug.Log("Not enough materials!");
            return;
        }

        switch (buildingData.buildingType)
        {
            case BuildType.Bank:
                BuildBank();
                break;
            case BuildType.Archers:
                BuildArchers();
                break;
            case BuildType.Swordsmen:
                BuildSwordsmen();
                break;
            case BuildType.Siege:
                BuildSiege();
                break;
            case BuildType.Wall:
                BuildWall();
                break;
        }
        RemoveMaterials(buildingData);
        UIHandler.instance.HideAllMenus();
    }

    private bool PlayerHasEnoughMaterials(BuildingData buildingData)
    {
        bool hasEnough = MaterialCounter.WoodCounter >= buildingData.buildingWoodRequirement && MaterialCounter.RockCounter >= buildingData.buildingRockRequirement && MaterialCounter.StringCounter >= buildingData.buildingStringRequirement && MaterialCounter.IronCounter >= buildingData.buildingIronRequirement;

        return hasEnough;

    }

    private void BuildBank()
    {
        BuildingCounter.BankAmount++; 
        Debug.Log($"Banks: {BuildingCounter.BankAmount}");
    }
    private void BuildArchers()
    {
        BuildingCounter.ArchersAmount++;
        Debug.Log($"Archers: {BuildingCounter.ArchersAmount}");
    }
    private void BuildSwordsmen()
    {
        BuildingCounter.SwordsmenAmount++;
        Debug.Log($"Swordsmen: {BuildingCounter.SwordsmenAmount}");
    }
    private void BuildSiege()
    {
        BuildingCounter.SiegeAmount++;
        Debug.Log($"Siege: {BuildingCounter.SiegeAmount}");

    }
    private void BuildWall()
    {
        BuildingCounter.WallAmount++;
        Debug.Log($"Walls: {BuildingCounter.WallAmount}");
    }

    private void RemoveMaterials(BuildingData buildingData)
    {
        MaterialCounter.WoodCounter -= buildingData.buildingWoodRequirement;
        MaterialCounter.RockCounter -= buildingData.buildingRockRequirement;
        MaterialCounter.StringCounter -= buildingData.buildingStringRequirement;
        MaterialCounter.IronCounter -= buildingData.buildingIronRequirement;
    }
}
