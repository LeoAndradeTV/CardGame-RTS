using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMotor : MonoBehaviour
{
    public readonly int IsStopped = Animator.StringToHash("b_isStopped");

    protected NavMeshAgent agent;
    [SerializeField] float stoppingDistance;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        animator.SetBool(IsStopped, agent.velocity.magnitude == 0f);
    }

    public virtual void MoveToPoint(Vector3 point)
    {
        Vector3 finalDestination = new Vector3(point.x + transform.position.x, point.y, point.z);
        agent.SetDestination(finalDestination);
        agent.stoppingDistance = stoppingDistance;
    }
}
