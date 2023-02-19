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
        motor.animator.SetBool(motor.IsStopped, false);

    }

    public override void ExitState(AttackStateManager manager)
    {
        motor.animator.SetBool(motor.IsStopped, true);
        manager.SwitchState(manager.attackingState);
    }

    public override void UpdateState(AttackStateManager manager)
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
