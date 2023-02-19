using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : GameStateAbstract
{
    private UnitMotor[] unitsOnTheBoard;

    public override void EnterState(GameStateManager manager)
    {
        Debug.Log("Hello from attack state");
        manager.hasAttacked = false;
    }

    public override void ExitState(GameStateManager manager)
    {
        UIHandler.instance.ChangeToBoardView();
        manager.SwitchState(manager.GetLastState());
    }

    public override void UpdateState(GameStateManager manager)
    {
        unitsOnTheBoard = GameObject.FindObjectsOfType<UnitMotor>();
        if (unitsOnTheBoard.Length == 0 && manager.hasAttacked)
        {
            ExitState(manager);
        }
    }
}
