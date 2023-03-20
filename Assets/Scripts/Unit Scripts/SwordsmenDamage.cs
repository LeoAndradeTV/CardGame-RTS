using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmenDamage : MonoBehaviour
{
    int damage;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private HealthBar healthBar;

    private PhotonView healthBarView;

    private void Start()
    {
        damage = Random.Range(minDamage, maxDamage);
        healthBarView = PhotonView.Find(GameManager.instance.healthBarId);

    }

    public void DealDamage()
    {
        if (GameStateManager.instance.activePlayerNumber == Support.GetPlayerRoomId(GameStateManager.instance.player))
        {
            AttackStateManager.instance.targetPlayer.CustomProperties["CurrentHealth"] = (int)AttackStateManager.instance.targetPlayer.CustomProperties["CurrentHealth"] - damage;
            int currentHealth = (int)AttackStateManager.instance.targetPlayer.CustomProperties["CurrentHealth"];
            healthBarView.RPC("SetHealth", RpcTarget.All, currentHealth);
        }
        
    }
    
}
