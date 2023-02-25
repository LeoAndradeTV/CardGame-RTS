using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public static class GameDeckDatabase
{
    public static List<CardData> buyDeck = new List<CardData>();
    public static List<CardData> cardsToPlace = new List<CardData>();
    public static List<GameObject> cardsOnBank = new List<GameObject>();
    public static List<CardData> cardDatasOnBank = new List<CardData>();
    public static bool[] locationIsFilled;
    public static List<int> emptyIndexes = new List<int>();

    public static void GenerateBuyDeck(List<CardData> cardsToBuy)
    {
        foreach (CardData data in cardsToBuy)
        {
            if (data.cardStatus == CardStatus.Bought) { continue; }

            buyDeck.Add(data);
        }
        Debug.Log($"BuyDeck Count is: {buyDeck.Count}");
    }

    public static int GetEmptySpots()
    {
        emptyIndexes.Clear();
        int emptySpots = 0;
        for (int i = 0; i < locationIsFilled.Length; i++)
        {
            if (!locationIsFilled[i])
            {
                emptySpots++;
                Debug.Log($"Index that opened up was: {i}");
                emptyIndexes.Add(i);
            }
        }
        Debug.Log($"Empty Spots: {emptySpots}");
        return emptySpots;
    }

    public static void GetCardsToPlace()
    {
        cardsToPlace.Clear();
        int emptySpots = GetEmptySpots();
        for (int i = 0; i < emptySpots; i++)
        {
            int index = Random.Range(0, buyDeck.Count);
            Debug.Log(index);
            cardsToPlace.Add(buyDeck[index]);
            buyDeck.RemoveAt(index);
        }
    }
}
