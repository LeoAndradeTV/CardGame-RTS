using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCards : MonoBehaviour
{
    public static PlayerCards instance;

    [SerializeField] private CardData[] cardData;
    [SerializeField] private Card cardPrefab;
    private List<CardData> playerDeck = new List<CardData>();
    private List<CardData> playerDiscard = new List<CardData>();
    public List<Card> cardsInHand = new List<Card>();

    private bool deckIsEmpty => playerDeck.Count == 0;
    private bool handIsFull => cardsInHand.Count >= 5;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        CreateInitialDeck();
    }

    // Create every initial card
    public void CreateInitialDeck()
    {
        for (int i = 0; i < 7; i++)
        {
            playerDeck.Add(cardData[0]);
        }
        for (int i = 0; i < 3; i++)
        {
            playerDeck.Add(cardData[1]);
        }

        //TODO: Add one type of attack card
    }
    public void DrawCards()
    {
        for (int i = 0; i < 5; i++)
        {
            if (deckIsEmpty || handIsFull)
            {
                Debug.Log("Can't draw right now"); 
                return;
            }

            // Gets random card data from the deck
            int index = Random.Range(0, playerDeck.Count);
            CardData currentData = playerDeck[index];

            // Check to see if location in hand has card
            if (Table.Instance.locationIsFilled[i])
                continue;

            // Makes and places card
            Card card = Instantiate(cardPrefab, Table.Instance.cardLocations[i].position, Quaternion.Euler(90f, 0f, 0f));
            
            card.indexInHand = i;
            Table.Instance.locationIsFilled[i] = true;

            // Adds card to list
            cardsInHand.Add(card);
            card.SetUpCard(currentData);
            card.cardStatus = CardStatus.Bought;

            playerDeck.RemoveAt(index);

            SetDrawAndShuffleButtons(!deckIsEmpty);
        }
        UIHandler.instance.SetBuildButton(false);

    }
    public void DiscardCard(Card card)
    {
        playerDiscard.Add(card.currentData);
        cardsInHand.Remove(card);
        Table.Instance.locationIsFilled[card.indexInHand] = false;
        Destroy(card.gameObject);
    }
    public void ShuffleCards()
    {
        while (playerDiscard.Count > 0)
        {
            playerDeck.Add(playerDiscard[0]);
            playerDiscard.RemoveAt(0);
        }
        SetDrawAndShuffleButtons(true);
    }
    public int GetCardsInHand()
    {
        return cardsInHand.Count;
    }
    private void SetDrawAndShuffleButtons(bool active)
    {
        Table.Instance.shuffleButton.gameObject.SetActive(!active);
        Table.Instance.drawButton.gameObject.SetActive(active);
    }
    private void OnEnable()
    {
        Actions.OnDrawCardsClicked += DrawCards;
        Actions.OnShuffleCardsClicked += ShuffleCards;
    }
    private void OnDisable()
    {
        Actions.OnDrawCardsClicked -= DrawCards;
        Actions.OnShuffleCardsClicked -= ShuffleCards;

    }
    public void AddCardToDiscardFromBank(Card card)
    {
        playerDiscard.Add(card.currentData);
    }
}
