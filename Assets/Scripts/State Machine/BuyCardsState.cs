using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCardsState : GameStateAbstract
{
    private Card[] cards;
    private bool isTurnEnded = false;

    public override void EnterState(GameStateManager manager)
    {
        Debug.Log("Hello from buy cards");
        UIHandler.instance.SetEndTurnButton(true);
        Actions.OnTurnEnded += ToggleTurnEnded;
        isTurnEnded = false;
        SetCardBankColliders(true);
    }


    public override void ExitState(GameStateManager manager)
    {
        Actions.OnTurnEnded -= ToggleTurnEnded;
        UIHandler.instance.SetEndTurnButton(false);
        SetCardBankColliders(false);
        manager.SwitchState(manager.endTurnState);
    }

    public override void UpdateState(GameStateManager manager)
    {
        if (isTurnEnded)
        {
            ExitState(manager);
        }
    }

    private void SetCardBankColliders(bool shouldBeActive)
    {
        cards = GameObject.FindObjectsOfType<Card>();
        foreach (Card card in cards)
        {
            if (card.cardStatus == CardStatus.Available)
            {
                card.GetComponent<BoxCollider>().enabled = shouldBeActive;
            }
        }
    }

    private void ToggleTurnEnded()
    {
        isTurnEnded = true;
    }
}
