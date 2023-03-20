using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMotor : MonoBehaviour
{
    public readonly int IsStopped = Animator.StringToHash("b_isStopped");
    public readonly int IsAttacking = Animator.StringToHash("b_isAttacking");

    private LaunchProjectile launchProjectile;
    protected NavMeshAgent agent;
    [SerializeField] float stoppingDistance;
    public Animator animator;
    public bool hasTarget;
    public bool AgentIsStopped => agent.velocity.magnitude == 0f;
    public float AgentRemainingDistance => agent.remainingDistance;
    public bool hasCalculatedPath => agent.pathPending == false;
    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        launchProjectile = GetComponent<LaunchProjectile>();
        
    }
    private void Update()
    {
        if (!AgentIsStopped)
        {
            if (AgentRemainingDistance < stoppingDistance)
            {
                agent.velocity = Vector3.zero;
            }
        }
        if (isAttacking)
        {
            //launchProjectile.Launch();
        }
    }

    public virtual void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
        agent.stoppingDistance = stoppingDistance;
    }

    public float GetStoppingDistance()
    {
        return stoppingDistance;
    }
}
