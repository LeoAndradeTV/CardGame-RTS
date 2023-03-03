using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class HealthBar : MonoBehaviour
{
    public PhotonView photonView;
    public Slider slider;
    public TMP_Text healthText;
    public TMP_Text playerName;
    public Image fill;
    public int maxHealth = 10000;
    public int currentHealth;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

    }

    private void OnEnable()
    {
        //SetUpHealthBar(AttackStateManager.instance.targetPlayer);
        photonView.RPC("SetUpHealthBar", RpcTarget.All, AttackStateManager.instance.targetPlayer);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = Color.green;
    }

    [PunRPC]
    public void SetHealth(int health)
    {
        slider.value = health;
        healthText.text = $"{health}/10000";
        if (health < 4000 && health > 1000)
        {
            fill.color = Color.yellow;
            return;
        }
        if (health < 1000)
        {
            fill.color = Color.red;
        }
    }

    [PunRPC]
    public void SetUpHealthBar(Player player)
    {
        currentHealth = (int)player.CustomProperties["CurrentHealth"];
        playerName.text = player.NickName;
    }
}
