using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class Card : MonoBehaviour
{

    private CardType cardType;
    public CardData currentData { get; private set; }
    public CardStatus cardStatus;
    public int indexInHand;
    public int price;
    private bool canSelectCard = true;
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
        if (!UIHandler.instance.allMenusAreClosed || !canSelectCard)
            return;

        if (cardStatus == CardStatus.Bought)
        {
            UIHandler.instance.ShowAndSetCardPlayMenu(this);
            return;
        }

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
                PlayGold();
                break;
            case CardType.OneFarmer:
                HarvestMaterial(1, 1);
                break;
            case CardType.TwoFarmers:
                HarvestMaterial(2, 1);
                break;
            case CardType.ThreeFarmers:
                HarvestMaterial(3, 1);
                break;
            case CardType.MachineOne:
                HarvestMaterial(4, 1);
                break;
            case CardType.MachineTwo:
                HarvestMaterial(2, 2);
                break;
            case CardType.MachineThree:
                HarvestTwoWood();
                break;
            case CardType.MachineFour:
                HarvestTwoRock();
                break;
            case CardType.MachineFive:
                HarvestTwoString();
                break;
            case CardType.MachineSix:
                HarvestTwoIron();
                break;
            case CardType.Overtime:
                HarvestMaterial(1, 1);
                PlayerCards.instance.DrawCards();
                break;
            case CardType.RegularAttack:
                Attack();
                break;
        }
        Debug.Log(cardType);
    }

    private void Attack()
    {
        UIHandler.instance.HideAllMenus();
        UIHandler.instance.ShowAttackMenu();
        Debug.Log("Attack card played");
    }

    private void HarvestTwoIron()
    {
        GameManager.instance.materialsPerHarvest = 4;
        GameManager.instance.HarvestIron();
    }
    private void HarvestTwoString()
    {
        GameManager.instance.materialsPerHarvest = 4;
        GameManager.instance.HarvestString();
    }
    private void HarvestTwoRock()
    {
        GameManager.instance.materialsPerHarvest = 4;
        GameManager.instance.HarvestRock();
    }
    private void HarvestTwoWood()
    {
        GameManager.instance.materialsPerHarvest = 4;
        GameManager.instance.HarvestWood();
    }

    private void HarvestMaterial(int harvests, int materialsPerHarvest)
    {
        UIHandler.instance.HideAllMenus();
        UIHandler.instance.OpenMaterialMenu(materialsPerHarvest);
        UIHandler.instance.harvests = harvests;
    }

    private static void PlayGold()
    {
        UIHandler.instance.HideAllMenus();
        PlayerStats.Instance.GoldAmount += BuildingCounter.BankAmount * 3;

    }

    private void SetCardInteractable(bool interactable)
    {
        canSelectCard = interactable;
    }

    private void OnEnable()
    {
        Actions.ChangeCardInteractable += SetCardInteractable;
    }

    private void OnDisable()
    {
        Actions.ChangeCardInteractable -= SetCardInteractable;
    }
}
