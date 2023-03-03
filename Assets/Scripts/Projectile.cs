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

    private void OnEnable()
    {
        damage = Random.Range(minDamage, maxDamage);
        rb = GetComponent<Rigidbody>();
        GetComponent<Collider>().enabled = true;
    }

    public void DealDamage()
    {
        AttackStateManager.instance.targetPlayer.CustomProperties["CurrentHealth"] = (int)AttackStateManager.instance.targetPlayer.CustomProperties["CurrentHealth"] - damage;
        int currentHealth = (int)AttackStateManager.instance.targetPlayer.CustomProperties["CurrentHealth"];
        PhotonView healthBarView = PhotonView.Find(GameManager.instance.healthBarId);
        healthBarView.RPC("SetHealth", RpcTarget.All, currentHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DealDamage();
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
