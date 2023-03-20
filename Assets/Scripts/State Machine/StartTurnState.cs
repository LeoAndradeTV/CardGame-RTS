using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTurnState : GameStateAbstract
{
    private GameObject[] cards;

    public override void EnterState(GameStateManager manager)
    {
        cards = GameObject.FindGameObjectsWithTag("Card");
        foreach (GameObject card in cards)
        {
            if (card.GetComponent<Card>().cardStatus == CardStatus.Available)
            {
                card.GetComponent<Collider>().enabled = false;
            }
        }
        Table.Instance.GoldAmount += BuildingCounter.BankAmount * 5;
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
