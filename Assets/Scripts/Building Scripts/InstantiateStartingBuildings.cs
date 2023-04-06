using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InstantiateStartingBuildings : MonoBehaviour
{
    public List<GameObject> startingAreaPrefabs;

    private List<Vector3> startingAreasPositions = new List<Vector3>()
    {
        new Vector3(0,0,0),
        new Vector3(252f, 0, 256f),
        new Vector3(0f, 0f, 500f),
        new Vector3(-252f, 0f, 256f)
    };

    private List<Quaternion> startingQuaternions = new List<Quaternion>()
    {  
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,-90,0),
        Quaternion.Euler(0,180,0),
        Quaternion.Euler(0,90,0)
    };

    public List<Material> materials = new List<Material>();

    private List<StartingBuildInfo> startingBuildInfos = new List<StartingBuildInfo>();

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PlaceBuildings();
        }
    }
    
    public void PlaceBuildings()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            startingBuildInfos.Add(new StartingBuildInfo(startingAreasPositions[i], startingQuaternions[i], materials[i]));
        }
        for (int i = 0; i < startingAreasPositions.Count; i++)
        {
            PhotonNetwork.Instantiate(startingAreaPrefabs[i].name, startingBuildInfos[i].position, startingBuildInfos[i].rotation);
        }
    }
}
