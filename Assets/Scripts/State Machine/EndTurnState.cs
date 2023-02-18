using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnState : GameStateAbstract
{
    public override void EnterState(GameStateManager manager)
    {
        manager.SwitchState(manager.startTurnState);
    }

    public override void ExitState(GameStateManager manager)
    {
        
    }

    public override void UpdateState(GameStateManager manager)
    {
        
    }
}
