using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUnitsSpawner : MonoBehaviourPunCallbacks, IPunObservable
{
    private Player player;

    public GameObject[] unitPrefabs;
    private List<Vector3> spawnPositions = new List<Vector3>();

    private List<Vector3> startingOffsets = new List<Vector3>()
        {
            new Vector3(-60, 0, 0),
            new Vector3(70, 0, 15),
            new Vector3(60, 0, 145),
            new Vector3(-60, 0, 135)
        };

    private List<float> maxXList = new List<float>() { 60f, 30f, -60f, -20f};
    private List<float> maxZList = new List<float>() { 40f, 135f, 105f, 15f};

    private List<float> xOffsetList = new List<float> { 8, -8, -8, 8 };
    private List<float> zOffsetList = new List<float> { 8, 8, -8, -8 };

    private List<Quaternion> unitRotations = new List<Quaternion>()
    {
        Quaternion.Euler(0, 0, 0),
        Quaternion.Euler(0, 270, 0),
        Quaternion.Euler(0, 180, 0),
        Quaternion.Euler(0, 90, 0)
    };

    public void SpawnUnits(int archers, int swordsmen, int sieges)
    {
        player = PhotonNetwork.LocalPlayer;
        spawnPositions.Clear();
        buildSpawnPositions(archers + swordsmen + sieges);
        SpawnSieges(sieges);
        SpawnArchers(archers);
        SpawnSwordsmen(swordsmen);
    }


    private void InstantiateUnit(int unitNumber)
    {
        PhotonNetwork.Instantiate(unitPrefabs[unitNumber].name, unitPrefabs[unitNumber].transform.position + spawnPositions[0], unitRotations[PhotonNetwork.LocalPlayer.ActorNumber - 1]);
        spawnPositions.RemoveAt(0);
    }

    private void SpawnSieges(int sieges)
    {
        for (int j = 0; j < sieges; j++)
        {
            InstantiateUnit(2);
        }
    }
    private void SpawnArchers(int archers)
    {
        for (int j = 0; j < archers; j++)
        {
            InstantiateUnit(0);
        }
    }

    private void SpawnSwordsmen(int swordsmen)
    {
        for (int j = 0; j < swordsmen; j++)
        {
            InstantiateUnit(1);
        }
    }

    public void buildSpawnPositions(int amount)
    {
        Vector3 spawnPos = startingOffsets[player.ActorNumber - 1];
        spawnPositions.Add(spawnPos);
        for (int i = 0; i < amount; i++)
        {
            if (player.ActorNumber == 1)
            {
                if (spawnPos.x < maxXList[player.ActorNumber - 1])
                {
                    spawnPos.x += xOffsetList[player.ActorNumber - 1];
                }
                else
                {
                    spawnPos.z += zOffsetList[player.ActorNumber - 1];
                    spawnPos.x = startingOffsets[player.ActorNumber - 1].x;
                }
                if (spawnPos.z > maxZList[player.ActorNumber - 1])
                {
                    Debug.Log("Most units deployed: 96");
                }
            }
            else if (player.ActorNumber == 2)
            {
                if (spawnPos.z < maxZList[player.ActorNumber - 1])
                {
                    spawnPos.z += zOffsetList[player.ActorNumber - 1];
                }
                else
                {
                    spawnPos.x += xOffsetList[player.ActorNumber - 1];
                    spawnPos.z = startingOffsets[player.ActorNumber - 1].z;
                }
                if (spawnPos.x < maxXList[player.ActorNumber - 1])
                {
                    Debug.Log("Most units deployed: 96");
                }
            } else if (player.ActorNumber == 3)
            {
                if (spawnPos.x < maxXList[player.ActorNumber - 1])
                {
                    spawnPos.x += xOffsetList[player.ActorNumber - 1];
                }
                else
                {
                    spawnPos.z += zOffsetList[player.ActorNumber - 1];
                    spawnPos.x = startingOffsets[player.ActorNumber - 1].x;
                }
                if (spawnPos.z > maxZList[player.ActorNumber - 1])
                {
                    Debug.Log("Most units deployed: 96");
                }
            } else if (player.ActorNumber == 4)
            {
                if (spawnPos.z < maxZList[player.ActorNumber - 1])
                {
                    spawnPos.z += xOffsetList[player.ActorNumber - 1];
                }
                else
                {
                    spawnPos.x += xOffsetList[player.ActorNumber - 1];
                    spawnPos.z = startingOffsets[player.ActorNumber - 1].x;
                }
                if (spawnPos.x > maxXList[player.ActorNumber - 1])
                {
                    Debug.Log("Most units deployed: 96");
                }
            }         
            spawnPositions.Add(spawnPos);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
