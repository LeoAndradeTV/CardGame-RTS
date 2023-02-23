using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    [SerializeField] private PlayerListingsMenu _playerListingsMenu;
    [SerializeField] private LeaveRoomMenu _leaveRoomMenu;
    [SerializeField] private TMP_Text _roomNameText;

    public LeaveRoomMenu LeaveRoomMenu { get { return _leaveRoomMenu; } }

    private RoomsCanvases roomsCanvases;

    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
        _playerListingsMenu.FirstInitialize(canvases);
        _leaveRoomMenu.FirstInitialize(canvases);
    }

    public void Show()
    {
        SetRoomName();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void SetRoomName()
    {
        _roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }

}
