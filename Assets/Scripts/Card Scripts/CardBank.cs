using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CardBank : MonoBehaviourPunCallbacks
{
    private Player player;

    [SerializeField] private List<CardData> cardDataToBuy = new List<CardData>();
    public List<Transform> bankPlacementPoints = new List<Transform>();
    [SerializeField] private Card cardPrefab;

    public List<GameObject> cardsToPlace = new List<GameObject>();
    public List<GameObject> cardsOnCardBank = new List<GameObject>();
    public List<CardData> cardDatasOnCardBank = new List<CardData>();

    private PhotonView photonView;

    private bool placementPointsAreFull => cardsOnCardBank.Count == bankPlacementPoints.Count;
    private bool purchaseDeckIsEmpty => cardDataToBuy.Count == 0;

    // Start is called before the first frame update
    void Awake()
    {
        player = PhotonNetwork.LocalPlayer;
        photonView = GetComponent<PhotonView>();

        //PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        GameDeckDatabase.locationIsFilled = new bool[bankPlacementPoints.Count];

        GameDeckDatabase.GenerateBuyDeck(cardDataToBuy);

        if (PhotonNetwork.IsMasterClient)
        {
            GameDeckDatabase.GetCardsToPlace();
            PlaceCardsOnBank(GameDeckDatabase.cardsToPlace, GameDeckDatabase.emptyIndexes);
            //SynchronizeCards(viewIDs, GameDeckDatabase.cardsToPlace);
        }

        SetLocationsToFilled();
        
    }

    // Method to Photon.Instantiate this card with setup over the network
    public void PlaceCardsOnBank(List<CardData> data, List<int> indexes)
    {
        for (int i = 0; i < indexes.Count; i++)
        {
            GameObject card = PhotonNetwork.Instantiate(cardPrefab.gameObject.name, bankPlacementPoints[indexes[i]].position, Quaternion.Euler(90f, 0f, 0f));
            Card cardComponent = card.GetComponent<Card>();
            int viewId = card.GetPhotonView().ViewID;
            photonView.RPC("SynchronizeCards", RpcTarget.All, viewId, data[i].cardType, data[i].name, data[i].description, data[i].cardStatus, data[i].price, indexes[i]);
            photonView.RPC("PlaceCardsLocally", RpcTarget.All, viewId, indexes[i]);
        }
    }

    public void CardWasBought(Card card, int goldAmount)
    {
        // Check that player has enough gold to buy card
        if (goldAmount < card.price)
        {
            return;
        }

        // Find CardData that matches card name and set it up to go on player deck
        int index = cardDataToBuy.FindIndex(x => x.name == card.cardNameText.text);
        card.SetUpCard(cardDataToBuy[index]);
        card.cardStatus = CardStatus.Bought;
        PlayerCards.instance.AddCardToDiscardFromBank(card);

        // Set location in bank to not filled
        GameDeckDatabase.locationIsFilled[card.indexInHand] = false;

        // Substitute card on bank across the network
        photonView.RPC("DestroyCardLocally", RpcTarget.All, card.photonView.ViewID);
        GameDeckDatabase.GetCardsToPlace();
        PlaceCardsOnBank(GameDeckDatabase.cardsToPlace, GameDeckDatabase.emptyIndexes);


        // Reset all locations to filled
        SetLocationsToFilled();
    }

    [PunRPC]
    private void PlaceCardsLocally(int viewId, int index)
    {
        GameObject card = PhotonView.Find(viewId).gameObject;
        int caseNumber = player.ActorNumber - 1;
        switch (caseNumber)
        {
            case 0:
                card.transform.position = GameManager.instance.player1CardBank[index].transform.position;
                card.transform.rotation = GameManager.instance.player1CardBank[index].transform.rotation;
                break;
            case 1:
                card.transform.position = GameManager.instance.player2CardBank[index].transform.position;
                card.transform.rotation = GameManager.instance.player2CardBank[index].transform.rotation;
                break;
            case 2:
                card.transform.position = GameManager.instance.player3CardBank[index].transform.position;
                card.transform.rotation = GameManager.instance.player3CardBank[index].transform.rotation;
                break;
            case 3:
                card.transform.position = GameManager.instance.player4CardBank[index].transform.position;
                card.transform.rotation = GameManager.instance.player4CardBank[index].transform.rotation;
                break;
            default:
                break;
        }
        Debug.Log($"Player number is {player.ActorNumber} Card position is {card.transform.position}, card rotation is {card.transform.rotation}");
    }

    [PunRPC]
    private void DestroyCardLocally(int viewID)
    {
        GameObject cardToDestroy = PhotonView.Find(viewID).gameObject;
        Destroy(cardToDestroy);
    }

    private void SetLocationsToFilled()
    {
        for (int i = 0; i < bankPlacementPoints.Count; i++)
        {
            GameDeckDatabase.locationIsFilled[i] = true;
        }
    }

    [PunRPC]
    public void SynchronizeCards(int id, CardType type, string name, string description, CardStatus status, int money, int indexInHand)
    {
        PhotonView.Find(id).gameObject.GetComponent<Card>().SyncAcrossNetwork(type, name, description, status, money, indexInHand);  
    }

    public override void OnEnable()
    {
        base.OnEnable();
        Actions.OnCardBought += CardWasBought;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        Actions.OnCardBought -= CardWasBought;
    }
}
