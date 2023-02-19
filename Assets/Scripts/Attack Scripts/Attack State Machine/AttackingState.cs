using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : AttackBaseState
{
    private UnitMotor[] unitsOnTheBoard;
    private UnitMotor motor;

    public override void EnterState(AttackStateManager manager)
    {
        Debug.Log("Attacking State");
        motor = manager.GetComponent<UnitMotor>();
        motor.animator.SetBool(motor.IsAttacking, true);
        motor.isAttacking = true;
    }

    public override void ExitState(AttackStateManager manager)
    {
        motor.animator.SetBool(motor.IsAttacking, false);
        motor.isAttacking = false;
        manager.SwitchState(manager.deploymentState);
    }

    public override void UpdateState(AttackStateManager manager)
    {
        unitsOnTheBoard = GameObject.FindObjectsOfType<UnitMotor>();
        if (unitsOnTheBoard.Length == 0)
        {
            ExitState(manager);
        }
    }
}
