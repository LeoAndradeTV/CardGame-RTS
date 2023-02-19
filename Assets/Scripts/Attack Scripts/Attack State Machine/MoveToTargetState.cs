using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetState : AttackBaseState
{

    private UnitMotor motor;

    public override void EnterState(AttackStateManager manager)
    {
        motor = manager.GetComponent<UnitMotor>();
        Debug.Log("Move To Target State");

    }

    public override void ExitState(AttackStateManager manager)
    {
        manager.SwitchState(manager.attackingState);
    }

    public override void UpdateState(AttackStateManager manager)
    {
        if (motor.hasTarget && motor.AgentRemainingDistance <= motor.GetStoppingDistance() && motor.AgentIsStopped)
        {
            ExitState(manager);
        }
    }
}
