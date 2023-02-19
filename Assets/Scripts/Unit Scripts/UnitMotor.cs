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

    protected NavMeshAgent agent;
    [SerializeField] float stoppingDistance;
    public Animator animator;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform launchPosition;
    public float launchForce;
    public bool hasTarget;
    public float startProjectileTimer;
    private float projectileTimer;
    public bool AgentIsStopped => agent.velocity.magnitude == 0f;
    public float AgentRemainingDistance => agent.remainingDistance;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        projectileTimer = startProjectileTimer;
    }
    private void Update()
    {
        animator.SetBool(IsStopped, AgentIsStopped);
        if (hasTarget && AgentIsStopped)
        {
            projectileTimer -= Time.deltaTime;
            if (projectileTimer <= Mathf.Epsilon && launchPosition != null)
            {
                var proj = Instantiate(projectile, launchPosition.position, transform.rotation);
                proj.GetComponent<Rigidbody>().AddForce((transform.forward + Vector3.up) * launchForce, ForceMode.Impulse);
                projectileTimer = startProjectileTimer;
            }
        }
    }

    public virtual void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
        agent.stoppingDistance = stoppingDistance;
        Debug.Log(agent.hasPath);
    }

    public float GetStoppingDistance()
    {
        return stoppingDistance;
    }
}
