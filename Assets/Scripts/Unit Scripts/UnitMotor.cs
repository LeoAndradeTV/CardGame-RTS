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
    [SerializeField] Animator animator;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform launchPosition;
    public Vector3 launchAngle;
    public float launchForce;
    public bool hasTarget;
    public float startProjectileTimer;
    private float projectileTimer;
    private bool AgentIsStopped => agent.velocity.magnitude == 0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        projectileTimer = startProjectileTimer;
    }
    private void Update()
    {
        animator.SetBool(IsStopped, AgentIsStopped);
        animator.SetBool(IsAttacking, hasTarget && AgentIsStopped);
        if (hasTarget && AgentIsStopped)
        {
            projectileTimer -= Time.deltaTime;
            if (projectileTimer <= Mathf.Epsilon && launchPosition != null)
            {
                var proj = Instantiate(projectile, launchPosition.position, transform.rotation);
                launchAngle.x = -launchPosition.rotation.y;
                proj.GetComponent<Rigidbody>().AddForce((transform.forward + Vector3.up) * launchForce, ForceMode.Impulse);
                projectileTimer = startProjectileTimer;
            }
        }
    }

    public virtual void MoveToPoint(Vector3 point)
    {
      //Vector3 finalDestination = new Vector3(point.x + transform.position.x, point.y, point.z);
        agent.SetDestination(point);
        agent.stoppingDistance = stoppingDistance;
    }
}
