using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : GameStateAbstract
{
    public override void EnterState(GameStateManager manager)
    {
        Debug.Log("Hello from attack state");
    }

    public override void ExitState(GameStateManager manager)
    {
        ;
    }

    public override void UpdateState(GameStateManager manager)
    {
        
    }
}
