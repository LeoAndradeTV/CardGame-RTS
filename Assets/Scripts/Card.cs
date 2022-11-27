using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    private CardType cardType;
    public CardData currentData { get; private set; }
    public int indexInHand;
    [SerializeField] private TMP_Text cardNameText;
    [SerializeField] private TMP_Text cardDescriptionText;


    public void SetUpCard(CardData card)
    {
        currentData = card;
        cardType = card.CardType;
        cardNameText.text = card.name;
        cardDescriptionText.text = card.description;
    }

    private void OnMouseDown()
    {   
        if (!UIHandler.instance.cardMenuIsOpen)
            UIHandler.instance.ShowAndSetCardPlayMenu(this);
    }

    public void Play()
    {
        switch (cardType)
        {
            case CardType.Gold:
                Table.Instance.GoldAmount += 5;
                break;
        }
    }

    public void TestMethod(CardType cardType)
    {
        Debug.Log("We ran this");
    }
}
