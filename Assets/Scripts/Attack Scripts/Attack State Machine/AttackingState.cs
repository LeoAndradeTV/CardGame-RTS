using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : AttackBaseState
{

    public override void EnterState(AttackStateManager manager)
    {
        foreach (UnitMotor motor in manager.unitsOnTheBoard)
        {
            motor.animator.SetBool(motor.IsAttacking, true);
            motor.gameObject.GetComponent<DestroyUnit>().enabled = true;
            motor.isAttacking = true;
        }
    }

    public override void ExitState(AttackStateManager manager)
    {
        foreach (UnitMotor motor in manager.unitsOnTheBoard)
        {
            motor.animator.SetBool(motor.IsAttacking, false);
            motor.isAttacking = false;
        }
        manager.SwitchState(manager.deploymentState);
    }

    public override void UpdateState(AttackStateManager manager)
    {
        UnitMotor[] unitsLeft = GameObject.FindObjectsOfType<UnitMotor>();
        if (unitsLeft.Length == 0)
        {
            ExitState(manager);
        }
    }
}
