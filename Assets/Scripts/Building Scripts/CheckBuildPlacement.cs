using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class CheckBuildPlacement : MonoBehaviour
{
    BuildPlacement buildPlacement;

    private Player player;

    private float buildZMax = 180f;

    void Start()
    {
        player = PhotonNetwork.LocalPlayer;
        buildPlacement = GameObject.Find("BuildPlacement").GetComponent<BuildPlacement>();
    }

    private void Update()
    {
        if (player.ActorNumber == 1)
        {
            buildPlacement.canPlace = buildPlacement.pos.z < buildZMax;
            return;
        }
        if (player.ActorNumber == 2)
        {
            buildPlacement.canPlace = buildPlacement.pos.x > 70f;
            return;
        }
        if (player.ActorNumber == 3)
        {
            buildPlacement.canPlace = buildPlacement.pos.z > 320;
            return;
        }
        if (player.ActorNumber == 4)
        {
            buildPlacement.canPlace = buildPlacement.pos.x < -70f;
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("BuiltObject"))
        {
            buildPlacement.canPlace = false;
        } else if (other.gameObject.CompareTag("Vegetation") && buildPlacement.pos.z < buildZMax) 
        {
            buildPlacement.canPlace = true;
            buildPlacement.overlappingGameObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BuiltObject"))
        {
            buildPlacement.canPlace = true;
        }
        else if (other.gameObject.CompareTag("Vegetation"))
        {
            buildPlacement.canPlace = true;
            buildPlacement.overlappingGameObject = null;
        }
    }
}
