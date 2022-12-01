using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private BuildPlacement buildPlacement;

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
        UIHandler.instance.ChangeToTableView();
        UIHandler.instance.HideAllMenus();
    }

    private bool PlayerHasEnoughMaterials(BuildingData buildingData)
    {
        bool hasEnough = MaterialCounter.WoodCounter >= buildingData.buildingWoodRequirement && MaterialCounter.RockCounter >= buildingData.buildingRockRequirement && MaterialCounter.StringCounter >= buildingData.buildingStringRequirement && MaterialCounter.IronCounter >= buildingData.buildingIronRequirement;

        return hasEnough;

    }

    private void BuildBank()
    {
        buildPlacement.SelectObject(4);
        BuildingCounter.BankAmount++; 
        Debug.Log($"Banks: {BuildingCounter.BankAmount}");
    }
    private void BuildArchers()
    {
        buildPlacement.SelectObject(2);
        BuildingCounter.ArchersAmount++;
        Debug.Log($"Archers: {BuildingCounter.ArchersAmount}");
    }
    private void BuildSwordsmen()
    {
        buildPlacement.SelectObject(3);
        BuildingCounter.SwordsmenAmount++;
        Debug.Log($"Swordsmen: {BuildingCounter.SwordsmenAmount}");
    }
    private void BuildSiege()
    {
        buildPlacement.SelectObject(1);
        BuildingCounter.SiegeAmount++;
        Debug.Log($"Siege: {BuildingCounter.SiegeAmount}");

    }
    private void BuildWall()
    {
        buildPlacement.SelectObject(0);
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
