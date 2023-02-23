using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    private const string GAMEPLAY_LEVEL_NAME = "GameplayScene";
    private const string IS_READY_HASHTABLE_KEY = "IsReady";


    [SerializeField] private PlayerListing playerListingsPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject startButton;
    [SerializeField] private TMP_Text readyUpText;

    private ExitGames.Client.Photon.Hashtable myProperties = new ExitGames.Client.Photon.Hashtable();

    private List<PlayerListing> listings = new List<PlayerListing>();
    private RoomsCanvases _roomsCanvases;
    private bool ready = false;

    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SetReadyUp(false);
        GetCurrentRoomPlayers();
        ShowStartButton();
        myProperties[IS_READY_HASHTABLE_KEY] = false;
        PhotonNetwork.SetPlayerCustomProperties(myProperties);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < listings.Count; i++)
        {
            Destroy(listings[i].gameObject);
        }
        listings.Clear();
    }

    private void SetReadyUp(bool state)
    {
        ready = state;
        readyUpText.text = ready ? "Cancel" : "Ready Up!";
    }

    private void ShowStartButton()
    {
        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected) { return; }

        if(PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null) { return; }

        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
        
    }

    private void AddPlayerListing(Player player)
    {
        int index = listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            listings[index].SetPlayerInfo(player);
        }
        else
        {
            PlayerListing listing = Instantiate(playerListingsPrefab, content);
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                listings.Add(listing);
            }
        }
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(listings[index].gameObject);
            listings.RemoveAt(index);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _roomsCanvases.CurrentRoomCanvas.LeaveRoomMenu.OnClick_LeaveRoom();
    }

    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < listings.Count; i++)
            {
                if (listings[i].Player != PhotonNetwork.LocalPlayer)
                {
                    if (!listings[i].Ready)
                        return;
                }
            }

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(GAMEPLAY_LEVEL_NAME);
        }
    }

    public void OnClick_Ready()
    {
        SetReadyUp(!ready);
        myProperties[IS_READY_HASHTABLE_KEY] = ready;
        PhotonNetwork.SetPlayerCustomProperties(myProperties);
        base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.All, PhotonNetwork.LocalPlayer, ready);
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        int index = listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            listings[index].Ready = ready;
            //if (myProperties.ContainsKey(IS_READY_HASHTABLE_KEY))
            //{

            //    myProperties[IS_READY_HASHTABLE_KEY] = ready;
            //    PhotonNetwork.SetPlayerCustomProperties(myProperties);
            //}
            //else
            //{
            //    myProperties[IS_READY_HASHTABLE_KEY] = ready;
            //    PhotonNetwork.SetPlayerCustomProperties(myProperties);
            //}
            listings[index].ReadyCheckMark.SetActive(ready);
            //Debug.Log($"{player.NickName} changed their IsReady key to: {player.CustomProperties["IsReady"]}");

        }
    }
}
