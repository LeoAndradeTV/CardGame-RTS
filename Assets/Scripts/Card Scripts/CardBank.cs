using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class CardBank : MonoBehaviourPunCallbacks
{

    [SerializeField] private List<CardData> cardDataToBuy = new List<CardData>();
    [SerializeField] private List<Transform> bankPlacementPoints = new List<Transform>();
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
        PhotonNetwork.Destroy(card.gameObject);
        GameDeckDatabase.GetCardsToPlace();
        PlaceCardsOnBank(GameDeckDatabase.cardsToPlace, GameDeckDatabase.emptyIndexes);

        // Reset all locations to filled
        SetLocationsToFilled();
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
