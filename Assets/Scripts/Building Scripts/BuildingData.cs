using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Data", menuName = "Building Data")]
public class BuildingData : ScriptableObject
{
    public BuildType buildingType;
    public string buildingName;
    public string buildingDescription;
    public int buildingWoodRequirement;
    public int buildingRockRequirement;
    public int buildingStringRequirement;
    public int buildingIronRequirement;
}
