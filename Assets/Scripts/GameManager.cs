using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int materialsPerHarvest = 1;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //FOR TESTING ONLY
        PlayerStats.Instance.GoldAmount = 100;
    }

    public void StartTurn()
    {
        PlayerStats.Instance.GoldAmount += BuildingCounter.BankAmount * 3;
        PlayerCards.instance.DrawCards();
    }

    public void HarvestWood()
    {
        MaterialCounter.WoodCounter += materialsPerHarvest;
        UIHandler.instance.CheckIfDoneHarvesting();
    }
    public void HarvestRock()
    {
        MaterialCounter.RockCounter += materialsPerHarvest;
        UIHandler.instance.CheckIfDoneHarvesting();
    }
    public void HarvestString()
    {
        MaterialCounter.StringCounter += materialsPerHarvest;
        UIHandler.instance.CheckIfDoneHarvesting();
    }
    public void HarvestIron()
    {
        MaterialCounter.IronCounter += materialsPerHarvest;
        UIHandler.instance.CheckIfDoneHarvesting();
    }
}
