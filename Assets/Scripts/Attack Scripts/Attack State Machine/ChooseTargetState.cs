using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTargetState : AttackBaseState
{
    Camera cam;

    public LayerMask targetsMask;

    public override void EnterState(AttackStateManager manager)
    {
        cam = Camera.main;
        targetsMask = LayerMask.GetMask("Target Layer");
        foreach (UnitMotor motor in manager.unitsOnTheBoard)
        {
            motor.animator.SetBool(motor.IsStopped, true);
        }  
        GameStateManager.instance.hasAttacked = true;
        Debug.Log("Choose Target State");

    }

    public override void ExitState(AttackStateManager manager)
    {
        manager.SwitchState(manager.moveToTargetState);
    }

    public override void UpdateState(AttackStateManager manager)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000, targetsMask))
            {
                // move our player
                
                Vector3 moveLocation = new Vector3 (hit.point.x, 0f, hit.point.z);
                foreach (UnitMotor motor in manager.unitsOnTheBoard)
                {
                    motor.MoveToPoint(moveLocation);
                    motor.hasTarget = true;
                }
                ExitState(manager);
            }
        }
    }
}
