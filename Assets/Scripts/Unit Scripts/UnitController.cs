using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMotor))]
public class UnitController : MonoBehaviour
{
    Camera cam;
    UnitMotor motor;

    public LayerMask targetsMask = LayerMask.GetMask("Target Layer");
    
    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<UnitMotor>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000, targetsMask))
            {
                // move our player
                motor.MoveToPoint(hit.point);
                motor.hasTarget = true;
            }
        }
    }
}
