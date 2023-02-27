using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;

    [SerializeField] private GameObject playerBoardPrefab;
    [SerializeField] private GameObject cardBankPrefab;
    [SerializeField] private GameObject fightingAreaPrefab;

    public List<Vector3> boardPositions = new List<Vector3>();
    public List<Quaternion> boardRotations = new List<Quaternion>();
    public List<Vector3> bankPositions = new List<Vector3>();
    public List<Quaternion> bankRotations = new List<Quaternion>();
    public List<Vector3> cameraOnBoardPositions = new List<Vector3>();
    public List<Quaternion> cameraOnBoardRotations = new List<Quaternion>();
    public List<Quaternion> cardRotations = new List<Quaternion>();

    public List<Transform> player1CardBank = new List<Transform>();
    public List<Transform> player2CardBank = new List<Transform>();
    public List<Transform> player3CardBank = new List<Transform>();
    public List<Transform> player4CardBank = new List<Transform>();

    public List<List<Transform>> playerBankCardsTransforms = new List<List<Transform>>();

    private Player player;

    public static GameManager instance;
    public int materialsPerHarvest = 1;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        player = PhotonNetwork.LocalPlayer;

        playerBankCardsTransforms.Add(player1CardBank);
        playerBankCardsTransforms.Add(player2CardBank);
        playerBankCardsTransforms.Add(player3CardBank);
        playerBankCardsTransforms.Add(player4CardBank);

        Instantiate(playerBoardPrefab, boardPositions[player.ActorNumber - 1], boardRotations[player.ActorNumber - 1]);

        InitializeNetworkObjects();

        StartCoroutine(SetPlayersPositionAndRotation(
            bankPositions[player.ActorNumber - 1], 
            bankRotations[player.ActorNumber - 1], 
            cameraOnBoardPositions[player.ActorNumber - 1], 
            cameraOnBoardRotations[player.ActorNumber - 1]));

        //FOR TESTING ONLY
        Table.Instance.GoldAmount = 100;

    }

    private void InitializeNetworkObjects()
    {
        if (!PhotonNetwork.IsConnected) { return; }

        if (!PhotonNetwork.IsMasterClient) { return; }
        PhotonNetwork.Instantiate(cardBankPrefab.name, cardBankPrefab.transform.position, cardBankPrefab.transform.rotation);
        PhotonNetwork.Instantiate(fightingAreaPrefab.name, fightingAreaPrefab.transform.position, fightingAreaPrefab.transform.rotation);
    }

    private IEnumerator SetPlayersPositionAndRotation(Vector3 position, Quaternion rotation, Vector3 camPos, Quaternion camRot)
    {
        yield return new WaitForSeconds(3f);
        GameObject bank = GameObject.FindGameObjectWithTag("CardBank");
        bank.transform.position = position;
        bank.transform.rotation = rotation;
        Camera.main.transform.position = camPos;
        Camera.main.transform.rotation = camRot;
        SetUpCardsOnBank(bank.GetComponent<CardBank>(), playerBankCardsTransforms[player.ActorNumber - 1], cardRotations[player.ActorNumber - 1]);
        gameStateManager.gameObject.SetActive(true);
        Debug.Log($"Player actor number is {player.ActorNumber}, bank position is {position}");
    }

    public void SetUpCardsOnBank(CardBank bank, List<Transform> positions, Quaternion rotation)
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].transform.position = positions[i].position;
            cards[i].transform.rotation = rotation;
        }
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
