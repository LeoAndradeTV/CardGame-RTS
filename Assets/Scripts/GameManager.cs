using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;

    private Hashtable playerProperties = new Hashtable();

    [SerializeField] private GameObject playerBoardPrefab;
    [SerializeField] private GameObject cardBankPrefab;
    [SerializeField] private GameObject fightingAreaPrefab;
    [SerializeField] private Canvas healthBarCanvasPrefab;

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
    public HealthBar healthBar;
    public int healthBarId;

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

        List<Player> playersInRoom = Support.GetPlayersInRoom(PhotonNetwork.CurrentRoom.Players);
        List<Player> sortedPlayers = Support.SortListOfPlayers(playersInRoom);
        int index = sortedPlayers.FindIndex(x => x.NickName == player.NickName);
        foreach (Player p in sortedPlayers)
        {
            Debug.Log(p.NickName);
        }

        player.CustomProperties["RoomID"] = index;

        Debug.Log($"Player index is: {(int)player.CustomProperties["RoomID"]}");

        playerBankCardsTransforms.Add(player1CardBank);
        playerBankCardsTransforms.Add(player2CardBank);
        playerBankCardsTransforms.Add(player3CardBank);
        playerBankCardsTransforms.Add(player4CardBank);

        Instantiate(playerBoardPrefab, boardPositions[Support.GetPlayerRoomId(player)], boardRotations[Support.GetPlayerRoomId(player)]);

        InitializeNetworkObjects();

        StartCoroutine(SetPlayersPositionAndRotation(
            bankPositions[Support.GetPlayerRoomId(player)], 
            bankRotations[Support.GetPlayerRoomId(player)], 
            cameraOnBoardPositions[Support.GetPlayerRoomId(player)], 
            cameraOnBoardRotations[Support.GetPlayerRoomId(player)]));

        //FOR TESTING ONLY
        Table.Instance.GoldAmount = 0;

    }

    private void InitializeNetworkObjects()
    {
        if (!PhotonNetwork.IsConnected) { return; }

        if (!PhotonNetwork.IsMasterClient) { return; }
        PhotonNetwork.Instantiate(cardBankPrefab.name, cardBankPrefab.transform.position, cardBankPrefab.transform.rotation);
        PhotonNetwork.Instantiate(fightingAreaPrefab.name, fightingAreaPrefab.transform.position, fightingAreaPrefab.transform.rotation);
        PhotonNetwork.Instantiate(healthBarCanvasPrefab.name, healthBarCanvasPrefab.transform.position, healthBarCanvasPrefab.transform.rotation);
    }

    private IEnumerator SetPlayersPositionAndRotation(Vector3 position, Quaternion rotation, Vector3 camPos, Quaternion camRot)
    {
        yield return new WaitForSeconds(3f);
        GameObject bank = GameObject.FindGameObjectWithTag("CardBank");
        bank.transform.position = position;
        bank.transform.rotation = rotation;
        Camera.main.transform.position = camPos;
        Camera.main.transform.rotation = camRot;
        SetUpCardsOnBank(bank.GetComponent<CardBank>(), playerBankCardsTransforms[Support.GetPlayerRoomId(player)], cardRotations[Support.GetPlayerRoomId(player)]);
        gameStateManager.gameObject.SetActive(true);
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.gameObject.SetActive(false);
        healthBarId = healthBar.gameObject.GetPhotonView().ViewID;
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
