using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : GameStateAbstract
{
    private bool isDoneBuilding = false;

    public override void EnterState(GameStateManager manager)
    {
        Debug.Log("Hello from build state");
        isDoneBuilding = false;
        Actions.OnFinishedBuilding += ToggleDoneBuilding;
    }

    public override void ExitState(GameStateManager manager)
    {
        Actions.OnFinishedBuilding -= ToggleDoneBuilding;
        manager.SwitchState(manager.buyCardsState);
    }

    public override void UpdateState(GameStateManager manager)
    {
        if (isDoneBuilding)
        {
            ExitState(manager);
        }
    }

    public void ToggleDoneBuilding()
    {
        isDoneBuilding = true;
    }
}
