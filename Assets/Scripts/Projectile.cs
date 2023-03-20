using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

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
        Player targetPlayer = AttackStateManager.instance.targetPlayer;
        targetPlayer.CustomProperties["CurrentHealth"] = (int)targetPlayer.CustomProperties["CurrentHealth"] - damage;
        int currentHealth = (int)targetPlayer.CustomProperties["CurrentHealth"];
        healthBarView.RPC("SetHealth", RpcTarget.All, currentHealth);

        object[] data = new object[] { currentHealth, targetPlayer.NickName };
        PhotonNetwork.RaiseEvent(0, data, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameStateManager.instance.activePlayerNumber == Support.GetPlayerRoomId(GameStateManager.instance.player))
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
