using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardsState : GameStateAbstract
{
    private const string PLAYER_TABLE_TAG = "PlayerTable";

    private GameObject table;

    public override void EnterState(GameStateManager manager)
    {
        Debug.Log("Hello from play cards");

        table = GameObject.FindGameObjectWithTag(PLAYER_TABLE_TAG);
        table.GetComponent<Table>().drawButton.interactable = false;
    }

    public override void ExitState(GameStateManager manager)
    {
        UIHandler.instance.SetBuildsButton(true);
        table.GetComponent<Table>().drawButton.interactable = false;
        manager.SwitchState(manager.buildState);
    }

    public override void UpdateState(GameStateManager manager)
    {
        if (PlayerCards.instance.GetCardsInHand() == 0)
        {
            ExitState(manager);
        }
    }
}
