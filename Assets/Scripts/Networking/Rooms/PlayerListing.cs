using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject readyCheckMark;
    public GameObject ReadyCheckMark { get { return readyCheckMark; } }

    public ExitGames.Client.Photon.Hashtable myProperties = new ExitGames.Client.Photon.Hashtable();

    public Player Player {get; private set;}
    public bool Ready = false;

    public void SetPlayerInfo(Player player)
    {
        Player = player;
        SetPlayerText(player);
        ReadyCheckMark.SetActive(Ready);
    }

    private void SetPlayerText(Player player)
    {
        text.text = player.NickName;
    }

    public void SetIsReady(bool set)
    {
        myProperties["IsReady"] = set;
        PhotonNetwork.SetPlayerCustomProperties(myProperties);
    }
}
