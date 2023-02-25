using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cardBankPrefab;

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

        InitializeNetworkObjects();
    }

    private void InitializeNetworkObjects()
    {
        if (!PhotonNetwork.IsConnected) { return; }

        if (!PhotonNetwork.IsMasterClient) { return; }
        PhotonNetwork.Instantiate(cardBankPrefab.name, cardBankPrefab.transform.position, cardBankPrefab.transform.rotation);
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
