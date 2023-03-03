using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateManager : MonoBehaviour
{
    private Player player;

    public PhotonView photonView;

    public static AttackStateManager instance;

    public AttackBaseState currentState;
    public GameStateAbstract lastGameState;
    public AttackingState attackingState = new AttackingState();
    public ChooseTargetState chooseTargetState = new ChooseTargetState();
    public DeploymentState deploymentState = new DeploymentState();
    public MoveToTargetState moveToTargetState = new MoveToTargetState();

    public UnitMotor[] unitsOnTheBoard;

    public Player targetPlayer;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        player = PhotonNetwork.LocalPlayer;
        photonView = GetComponent<PhotonView>();
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

    [PunRPC]
    public void SetHealthBarActive(bool set, int viewId)
    {
        PhotonView.Find(viewId).gameObject.SetActive(set);
    }
}
