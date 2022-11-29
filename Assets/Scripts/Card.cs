using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    
    private CardType cardType;
    public CardData currentData { get; private set; }
    public CardStatus cardStatus;
    public int indexInHand;
    public int price;
    [SerializeField] private TMP_Text cardNameText;
    [SerializeField] private TMP_Text cardDescriptionText;
    [SerializeField] private TMP_Text cardPriceText;


    public void SetUpCard(CardData cardData)
    {
        currentData = cardData;
        cardType = cardData.CardType;
        cardNameText.text = cardData.name;
        cardDescriptionText.text = cardData.description;
        cardStatus = cardData.CardStatus;
        price = cardData.price;
        if (price == 0)
        {
            cardPriceText.gameObject.SetActive(false);
            return;
        }
        cardPriceText.text = $"Price: {price}";
    }

    private void OnMouseDown()
    {
        if (!UIHandler.instance.allMenusAreClosed)
            return;

        if (cardStatus == CardStatus.Bought)
        {
            UIHandler.instance.ShowAndSetCardPlayMenu(this);
            return;
        }

        // TODO: Open buy menu
        if (cardStatus == CardStatus.Available)
        {
            UIHandler.instance.ShowBuyMenu(this);
            return;
        }
        
    }

    public void Play()
    {
        switch (cardType)
        {
            case CardType.Gold:
                UIHandler.instance.HideAllMenus();
                Table.Instance.GoldAmount += 5;
                break;
            case CardType.OneFarmer:
                UIHandler.instance.HideAllMenus();
                UIHandler.instance.OpenMaterialMenu();
                UIHandler.instance.harvests = 1;
                break;
            case CardType.TwoFarmers:
                UIHandler.instance.HideAllMenus();
                UIHandler.instance.OpenMaterialMenu();
                UIHandler.instance.harvests = 2;
                break;
        }
    }
}
