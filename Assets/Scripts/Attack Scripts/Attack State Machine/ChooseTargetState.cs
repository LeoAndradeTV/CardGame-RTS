using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTargetState : AttackBaseState
{
    Camera cam;
    UnitMotor motor;

    public LayerMask targetsMask;

    public override void EnterState(AttackStateManager manager)
    {
        cam = Camera.main;
        targetsMask = LayerMask.GetMask("Target Layer");
        motor = manager.GetComponent<UnitMotor>();
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
                motor.MoveToPoint(moveLocation);
                motor.hasTarget = true;
                ExitState(manager);
            }
        }
    }
}
