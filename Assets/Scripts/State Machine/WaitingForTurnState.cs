using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class WaitingForTurnState : GameStateAbstract
{
    private const string PLAYER_TABLE_TAG = "PlayerTable";


    public override void EnterState(GameStateManager manager)
    {
        Debug.Log("Waiting to play");

        GameObject table = GameObject.FindGameObjectWithTag(PLAYER_TABLE_TAG);
        table.GetComponent<Table>().drawButton.interactable = false;

        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        foreach (GameObject card in cards)
        {
            if (card.GetComponent<Card>().cardStatus == CardStatus.Available)
            {
                card.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public override void ExitState(GameStateManager manager)
    {
        manager.SwitchState(manager.drawCardsState);
    }

    public override void UpdateState(GameStateManager manager)
    {
        if (manager.activePlayerNumber == manager.player.ActorNumber - 1)
        {
            ExitState(manager);
        }
    }
}
