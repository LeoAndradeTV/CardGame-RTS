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
        attackStateManager = MonoBehaviour.FindObjectOfType<AttackStateManager>();
        attackStateManager.enabled = true;
        Debug.Log($"Attack State Manager is null: {attackStateManager == null}");
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
