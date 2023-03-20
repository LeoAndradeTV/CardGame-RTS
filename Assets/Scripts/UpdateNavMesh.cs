using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UpdateNavMesh : MonoBehaviour
{
    private NavMeshSurface surface;

    private PhotonView view;

    // Start is called before the first frame update
    void Awake()
    {
        surface = GetComponentInChildren<NavMeshSurface>();
        surface.BuildNavMesh();
        view = GetComponent<PhotonView>();
    }


    private void BakeNavMesh(BuildingData data)
    {
        view.RPC("RPC_BakeNavMesh", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_BakeNavMesh()
    {
        surface.BuildNavMesh();

    }

    private void OnEnable()
    {
        Actions.OnBuildingBuilt += BakeNavMesh;
    }
    private void OnDisable()
    {
        Actions.OnBuildingBuilt -= BakeNavMesh;

    }
}
