using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class LocalHealthBar : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private Image fill;

    private int maxHealth = 10000;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerName.text = PhotonNetwork.LocalPlayer.NickName;
        SetHealth(maxHealth);
    }

    private void SetHealth(int health)
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

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == 0)
        {
            object[] data = (object[])photonEvent.CustomData;
            currentHealth = (int)data[0];
            string name = (string)data[1];
            if (name == PhotonNetwork.LocalPlayer.NickName)
            {
                SetHealth(currentHealth);
            }
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
