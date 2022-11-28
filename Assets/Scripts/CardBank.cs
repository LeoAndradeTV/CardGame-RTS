using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBank : MonoBehaviour
{
    [SerializeField] private List<CardData> cardDataToBuy = new List<CardData>();
    [SerializeField] private List<Transform> bankPlacementPoints = new List<Transform>();
    [SerializeField] private Card cardPrefab;

    private List<Card> cardsOnCardBank = new List<Card>();
    private bool[] locationIsFilled;

    private bool placementPointsAreFull => cardsOnCardBank.Count == bankPlacementPoints.Count;
    private bool purchaseDeckIsEmpty => cardDataToBuy.Count == 0;

    // Start is called before the first frame update
    void Awake()
    {
        locationIsFilled = new bool[bankPlacementPoints.Count];
        PlaceCardToBuy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlaceCardToBuy()
    {
        for (int i = 0; i < bankPlacementPoints.Count; i++)
        {
            if (placementPointsAreFull)
                return;

            int index = Random.Range(0, cardDataToBuy.Count);
            CardData currentData = cardDataToBuy[index];

            if (locationIsFilled[i])
                continue;

            Card cardToAdd = Instantiate(cardPrefab, bankPlacementPoints[i].position, Quaternion.Euler(90f, 0f, 0f));
            locationIsFilled[i] = true;

            cardsOnCardBank.Add(cardToAdd);
            cardToAdd.SetUpCard(currentData, CardStatus.Available);

            cardDataToBuy.RemoveAt(index);
        }
    }
}
