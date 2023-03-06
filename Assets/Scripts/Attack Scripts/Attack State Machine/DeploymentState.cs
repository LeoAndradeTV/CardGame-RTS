using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeploymentState : AttackBaseState
{
    public UnitMotor[] units;

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
        units = GameObject.FindObjectsOfType<UnitMotor>();
        if (units.Length > 0 )
        {
            manager.unitsOnTheBoard = units;
            ExitState(manager);
        }
    }
}
