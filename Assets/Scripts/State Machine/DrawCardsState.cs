using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DrawCardsState : GameStateAbstract
{
    private const string PLAYER_TABLE_TAG = "PlayerTable";

    private GameObject table;
    private Card[] playerCards;
    private bool hasDrawnCards = false;

    public override void EnterState(GameStateManager manager)
    {
        Debug.Log("Hello from draw cards");
        table = GameObject.FindGameObjectWithTag(PLAYER_TABLE_TAG);
        table.GetComponent<Table>().drawButton.interactable = true;
        Actions.OnDrawCardsClicked += ToggleHasDrawn;
        Actions.OnDrawCardsClicked += CheckNumberOfCards;
    }

    public override void ExitState(GameStateManager manager)
    {
        hasDrawnCards = false;
        Actions.OnDrawCardsClicked -= CheckNumberOfCards;
        Actions.OnDrawCardsClicked -= ToggleHasDrawn;
        manager.SwitchState(manager.playCardsState);
    }

    public override void UpdateState(GameStateManager manager)
    {
        if (hasDrawnCards && PlayerCards.instance.GetCardsInHand() == 5)
        {
            ExitState(manager);
        }
    }

    private void ToggleHasDrawn()
    {
        hasDrawnCards = true;
    }

    private void SetCardsInHandInteractable(bool interactable)
    {
        playerCards = GameObject.FindObjectsOfType<Card>();
        foreach (Card card in playerCards)
        {
            if (card.cardStatus == CardStatus.Bought)
            {
                card.GetComponent<BoxCollider>().enabled = interactable;
            }
        }
    }

    private void CheckNumberOfCards()
    {
        if (PlayerCards.instance.GetCardsInHand() != 5)
        {
            SetCardsInHandInteractable(false);
        } else
        {
            SetCardsInHandInteractable(true);
        }
    }
}
