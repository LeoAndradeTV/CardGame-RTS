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
                buildPlacement.SelectObject(4, buildingData);
                break;
            case BuildType.Archers:
                buildPlacement.SelectObject(2, buildingData);
                break;
            case BuildType.Swordsmen:
                buildPlacement.SelectObject(3, buildingData);
                break;
            case BuildType.Siege:
                buildPlacement.SelectObject(1, buildingData);
                break;
            case BuildType.Wall:
                buildPlacement.SelectObject(0, buildingData);
                break;
        }
        if (!CameraController.instance.canMoveCamera)
        {
            UIHandler.instance.ChangeToTableView();
        }
        UIHandler.instance.HideAllMenus();
    }

    private bool PlayerHasEnoughMaterials(BuildingData buildingData)
    {
        bool hasEnough = MaterialCounter.WoodCounter >= buildingData.buildingWoodRequirement && MaterialCounter.RockCounter >= buildingData.buildingRockRequirement && MaterialCounter.StringCounter >= buildingData.buildingStringRequirement && MaterialCounter.IronCounter >= buildingData.buildingIronRequirement;

        return hasEnough;

    }

    //public void BuildBank()
    //{
    //    BuildingCounter.BankAmount++;
    //    Debug.Log($"Banks: {BuildingCounter.BankAmount}");
    //    Debug.Log($"{PlayerStats.Instance.GoldAmount}");
    //}
    //public void BuildArchers()
    //{
    //    BuildingCounter.ArchersAmount++;
    //    Debug.Log($"Archers: {BuildingCounter.ArchersAmount}");
    //    Debug.Log($"{PlayerStats.Instance.rangedAttackStat}");
    //}
    //public void BuildSwordsmen()
    //{
    //    BuildingCounter.SwordsmenAmount++;
    //    Debug.Log($"Swordsmen: {BuildingCounter.SwordsmenAmount}");
    //    Debug.Log($"{PlayerStats.Instance.meleeAttackStat}");

    //}
    //public void BuildSiege()
    //{

    //    BuildingCounter.SiegeAmount++;
    //    Debug.Log($"Siege: {BuildingCounter.SiegeAmount}");
    //    Debug.Log($"{PlayerStats.Instance.rangedAttackStat}");


    //}
    //public void BuildWall()
    //{
    //    BuildingCounter.WallAmount++;
    //    Debug.Log($"Walls: {BuildingCounter.WallAmount}");
    //    Debug.Log($"{PlayerStats.Instance.protectionStat}");

    //}
}
