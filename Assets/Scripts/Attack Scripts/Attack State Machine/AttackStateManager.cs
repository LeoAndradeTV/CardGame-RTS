using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateManager : MonoBehaviour
{
    private Player player; 

    public AttackBaseState currentState;
    public GameStateAbstract lastGameState;
    public AttackingState attackingState = new AttackingState();
    public ChooseTargetState chooseTargetState = new ChooseTargetState();
    public DeploymentState deploymentState = new DeploymentState();
    public MoveToTargetState moveToTargetState = new MoveToTargetState();

    public UnitMotor[] unitsOnTheBoard;

    // Start is called before the first frame update
    void OnEnable()
    {
        player = PhotonNetwork.LocalPlayer;
        if (player.ActorNumber - 1 != GameStateManager.instance.activePlayerNumber) { return; }
        currentState = deploymentState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == null) { return; }
        if (player.ActorNumber - 1 != GameStateManager.instance.activePlayerNumber) { return; }
        currentState.UpdateState(this);
    }

    public void SwitchState(AttackBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
