using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomCustomPropertyGenerator : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private ExitGames.Client.Photon.Hashtable myProperties = new ExitGames.Client.Photon.Hashtable();

    private void SetCustomNumber()
    {
        System.Random rnd = new System.Random();
        int result = rnd.Next(0, 99);

        _text.text = result.ToString();

        myProperties["RandomNumber"] = result;
        PhotonNetwork.SetPlayerCustomProperties(myProperties);
        // PhotonNetwork.LocalPlayer.CustomProperties = myProperties;
    }

    public void OnClick_Button()
    {
        SetCustomNumber();
    }
}
