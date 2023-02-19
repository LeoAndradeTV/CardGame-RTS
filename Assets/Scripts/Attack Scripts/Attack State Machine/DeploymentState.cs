using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeploymentState : AttackBaseState
{
    public UnitMotor[] unitsOnTheBoard;

    public override void EnterState(AttackStateManager manager)
    {
        Debug.Log("Deployment State");
    }

    public override void ExitState(AttackStateManager manager)
    {
        manager.SwitchState(manager.chooseTargetState);
    }

    public override void UpdateState(AttackStateManager manager)
    {
        unitsOnTheBoard = GameObject.FindObjectsOfType<UnitMotor>();
        if (unitsOnTheBoard.Length > 0 )
        {
            ExitState(manager);
        }
    }
}
