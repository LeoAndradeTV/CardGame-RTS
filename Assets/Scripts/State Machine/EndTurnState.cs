using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnState : GameStateAbstract
{
    public override void EnterState(GameStateManager manager)
    {
        manager.photonView.RPC("ChangeActivePlayer", RpcTarget.All);
        ExitState(manager);
    }

    public override void ExitState(GameStateManager manager)
    {
        manager.SwitchState(manager.waitingForTurnState);
    }

    public override void UpdateState(GameStateManager manager)
    {
        
    }

  
}
