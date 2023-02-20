using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListing : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public Player Player {get; private set;}

    public void SetPlayerInfo(Player player)
    {
        Player = player;
        text.text = player.NickName;
    }
}
