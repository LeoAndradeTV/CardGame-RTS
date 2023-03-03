using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : GameStateAbstract
{
    private UnitMotor[] unitsOnTheBoard;
    private Projectile[] projectiles;
    private AttackStateManager attackStateManager;
    public override void EnterState(GameStateManager manager)
    {
        Debug.Log("Hello from attack state");
        GameManager.instance.healthBar = MonoBehaviour.FindObjectOfType<HealthBar>();
        Debug.Log($"Health Bar is null: {GameManager.instance.healthBar == null}");
        attackStateManager = MonoBehaviour.FindObjectOfType<AttackStateManager>();
        attackStateManager.enabled = true;
        manager.hasAttacked = false;
    }

    public override void ExitState(GameStateManager manager)
    {
        projectiles = GameObject.FindObjectsOfType<Projectile>();
        int rounds = projectiles.Length;
        for (int i = 0; i < rounds; i++)
        {
            MonoBehaviour.Destroy(projectiles[i].gameObject);
        }
        UIHandler.instance.ChangeToBoardView();
        attackStateManager.enabled = false;
        AttackStateManager.instance.targetPlayer = null;
        AttackStateManager.instance.photonView.RPC("SetHealthBarActive", RpcTarget.All, false, GameManager.instance.healthBarId);
        manager.SwitchState(manager.GetLastState());
    }

    public override void UpdateState(GameStateManager manager)
    {
        unitsOnTheBoard = GameObject.FindObjectsOfType<UnitMotor>();
        if (unitsOnTheBoard.Length == 0 && manager.hasAttacked)
        {
            ExitState(manager);
        }
    }

    
}
