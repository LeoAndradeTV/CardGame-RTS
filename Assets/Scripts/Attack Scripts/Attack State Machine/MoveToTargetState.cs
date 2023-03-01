using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetState : AttackBaseState
{
    int unitsThatReachedTarget = 0;
    public override void EnterState(AttackStateManager manager)
    {
        Debug.Log("Move To Target State");
        foreach (UnitMotor motor in manager.unitsOnTheBoard)
        {
            motor.animator.SetBool(motor.IsStopped, false);
        }

    }

    public override void ExitState(AttackStateManager manager)
    {
        foreach (UnitMotor motor in manager.unitsOnTheBoard)
        {
            motor.animator.SetBool(motor.IsStopped, true);
            manager.SwitchState(manager.attackingState);
        }
    }

    public override void UpdateState(AttackStateManager manager)
    {
        foreach (UnitMotor motor in manager.unitsOnTheBoard)
        {
            if (!motor.hasCalculatedPath)
            {
                return;
            }
            if (motor.hasTarget && motor.AgentRemainingDistance <= motor.GetStoppingDistance() && motor.AgentIsStopped)
            {
                ExitState(manager);
            }
        }
    }
}
