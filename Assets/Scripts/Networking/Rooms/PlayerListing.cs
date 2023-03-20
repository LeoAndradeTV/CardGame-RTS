using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    private const string IS_READY_HASHTABLE_KEY = "IsReady";

    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject readyCheckMark;
    public GameObject ReadyCheckMark { get { return readyCheckMark; } }


    public Player Player { get; private set; }
    public bool Ready = false;


    public override void OnDisable()
    {
        if (Player.CustomProperties.ContainsKey(IS_READY_HASHTABLE_KEY))
        {
            Player.CustomProperties[IS_READY_HASHTABLE_KEY] = false;
        }
        base.OnDisable();

    }

    public void SetPlayerInfo(Player player)
    {
        Player = player;
        SetPlayerText(player);
        if (player.CustomProperties.ContainsKey(IS_READY_HASHTABLE_KEY))
        {
            Ready = (bool)player.CustomProperties[IS_READY_HASHTABLE_KEY];
        }
        SetReadyCheckmark(Ready);
        //Debug.Log($"{player.NickName} bool has key: {player.CustomProperties.ContainsKey(IS_READY_HASHTABLE_KEY)} and result is {Ready}");
    }

    private void SetPlayerText(Player player)
    {
        text.text = player.NickName;

    }

    private void SetReadyCheckmark(bool set)
    {
        ReadyCheckMark.SetActive(set);

    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer != null && targetPlayer == Player)
        {
            if (changedProps.ContainsKey(IS_READY_HASHTABLE_KEY))
            {
                Ready = (bool)targetPlayer.CustomProperties[IS_READY_HASHTABLE_KEY];
                SetReadyCheckmark(Ready);
                Debug.Log($"{targetPlayer} is ready: {(bool)targetPlayer.CustomProperties[IS_READY_HASHTABLE_KEY]}");
            }
        }
    }
}
