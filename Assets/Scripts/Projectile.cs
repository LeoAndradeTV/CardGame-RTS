using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Projectile : MonoBehaviour
{
    private int damage;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;

    private Rigidbody rb;

    private PhotonView healthBarView;

    private void OnEnable()
    {
        damage = Random.Range(minDamage, maxDamage);
        rb = GetComponent<Rigidbody>();
        GetComponent<Collider>().enabled = true;
        healthBarView = PhotonView.Find(GameManager.instance.healthBarId);

    }

    public void DealDamage()
    {
        AttackStateManager.instance.targetPlayer.CustomProperties["CurrentHealth"] = (int)AttackStateManager.instance.targetPlayer.CustomProperties["CurrentHealth"] - damage;
        int currentHealth = (int)AttackStateManager.instance.targetPlayer.CustomProperties["CurrentHealth"];
        healthBarView.RPC("SetHealth", RpcTarget.All, currentHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameStateManager.instance.activePlayerNumber == GameStateManager.instance.player.ActorNumber - 1)
        {
            DealDamage();
        }
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (rb != null)
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }
        GetComponent<Collider>().enabled = false;

    }
}
