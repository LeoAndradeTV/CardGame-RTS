using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

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
        if (!UIHandler.instance.allMenusAreClosed)
            return;
        
        UIHandler.instance.ShowAndSetCardPlayMenu(this);
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
            case CardType.TwoFarmer:
                UIHandler.instance.HideAllMenus();
                UIHandler.instance.OpenMaterialMenu();
                UIHandler.instance.harvests = 2;
                break;
        }
    }
}
