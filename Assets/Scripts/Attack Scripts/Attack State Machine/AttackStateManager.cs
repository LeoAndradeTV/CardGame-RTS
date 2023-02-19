using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateManager : MonoBehaviour
{
    public AttackBaseState currentState;
    public GameStateAbstract lastGameState;
    public AttackingState attackingState = new AttackingState();
    public ChooseTargetState chooseTargetState = new ChooseTargetState();
    public DeploymentState deploymentState = new DeploymentState();
    public MoveToTargetState moveToTargetState = new MoveToTargetState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = deploymentState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(AttackBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
