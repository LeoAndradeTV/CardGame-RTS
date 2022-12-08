using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBank : MonoBehaviour
{
    public static CardBank instance;

    [SerializeField] private List<CardData> cardDataToBuy = new List<CardData>();
    [SerializeField] private List<Transform> bankPlacementPoints = new List<Transform>();
    [SerializeField] private Card cardPrefab;

    public List<Card> cardsOnCardBank = new List<Card>();
    public bool[] locationIsFilled;

    private bool placementPointsAreFull => cardsOnCardBank.Count == bankPlacementPoints.Count;
    private bool purchaseDeckIsEmpty => cardDataToBuy.Count == 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        locationIsFilled = new bool[bankPlacementPoints.Count];
        PlaceCardToBuy();
    }

    public void PlaceCardToBuy()
    {
        for (int i = 0; i < bankPlacementPoints.Count; i++)
        {
            if (placementPointsAreFull)
                return;

            int index = Random.Range(0, cardDataToBuy.Count);
            CardData currentData = cardDataToBuy[index];
            currentData.CardStatus = CardStatus.Available;

            if (locationIsFilled[i])
                continue;

            Card cardToAdd = Instantiate(cardPrefab, bankPlacementPoints[i].position, Quaternion.Euler(90f, 0f, 0f));
            locationIsFilled[i] = true;

            cardsOnCardBank.Add(cardToAdd);
            cardToAdd.SetUpCard(currentData);
            cardToAdd.indexInHand = i;

            cardDataToBuy.RemoveAt(index);

        }
    }
}
