using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTurnState : GameStateAbstract
{
    private Card[] cards;

    public override void EnterState(GameStateManager manager)
    {
        cards = GameObject.FindObjectsOfType<Card>();
        foreach (Card card in cards)
        {
            if (card.cardStatus == CardStatus.Available)
            {
                card.GetComponent<BoxCollider>().enabled = false;
            }
        }
        PlayerStats.Instance.GoldAmount += BuildingCounter.BankAmount * 5;
        Debug.Log("Hello from start turn");
        ExitState(manager);
    }

    public override void ExitState(GameStateManager manager)
    {
        manager.SwitchState(manager.drawCardsState);
    }

    public override void UpdateState(GameStateManager manager)
    {
        
    }
}
