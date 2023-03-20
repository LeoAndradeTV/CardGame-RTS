using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private BuildPlacement buildPlacement;

    private void OnEnable()
    {
        if (buildPlacement != null) { return; }
        buildPlacement = GameObject.FindGameObjectWithTag("FightingArea").GetComponentInChildren<BuildPlacement>();
    }

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
}
