using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class BuildPlacement : MonoBehaviourPunCallbacks
{
    private Player player;

    private PhotonView photonView;

    public GameObject[] objects;
    private GameObject pendingObject;
    private BuildingData pendingObjectBuildingData;

    public Vector3 pos;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Material[] materials;

    public float gridSize;
    private float rotateAmount = 45f;
    private Material startingMaterial;
    private MeshRenderer[] meshRenderers;

    [SerializeField] private Material[] materialsByPlayer;

    public bool canPlace = true;
    public GameObject overlappingGameObject;
    public bool mouseOverUIElement => EventSystem.current.IsPointerOverGameObject();

    public override void OnEnable()
    {
        base.OnEnable();
        photonView = GetComponent<PhotonView>();
        player = PhotonNetwork.LocalPlayer;
    }

    private void Update()
    {
        if (pendingObject != null)
        {
                pendingObject.transform.position = new Vector3(
                    RoundToNearestGrid(pos.x),
                    RoundToNearestGrid(pos.y),
                    RoundToNearestGrid(pos.z));


            pendingObject.transform.rotation = GameManager.instance.bankRotations[player.ActorNumber - 1];

            //pendingObject.transform.position = pos;
            if (Input.GetMouseButtonDown(0))
            {
                if (mouseOverUIElement)
                {
                    Destroy(pendingObject);
                }
                else
                {
                    if (canPlace)
                    {
                        PlaceObject(pendingObjectBuildingData.buildingType);
                        if (overlappingGameObject != null)
                        {
                            Destroy(overlappingGameObject);
                        }
                    }
                    
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                RotateObject();
            }
        }
        UpdateMaterials();
    }

    private void FixedUpdate()
    { 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 10000.0f, layerMask))
        {
            pos = hit.point;
        }
    }

    public void SelectObject(int index, BuildingData buildingData)
    {
        pendingObject = PhotonNetwork.Instantiate(objects[index].name, pos, transform.rotation);
        startingMaterial = pendingObject.GetComponentInChildren<MeshRenderer>().material;
        pendingObjectBuildingData = buildingData;
    }

    public void PlaceObject(BuildType building)
    {
        meshRenderers = pendingObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            if (building == BuildType.Wall)
            {
                renderer.material = startingMaterial;
                continue;
            }
            renderer.material = materialsByPlayer[player.ActorNumber-1];
        }
        int materialIndex = player.ActorNumber - 1;
        photonView.RPC("UpdateNewObjectMaterials", RpcTarget.All, pendingObject.GetPhotonView().ViewID, materialIndex);
        Actions.OnBuildingBuilt?.Invoke(pendingObjectBuildingData);

        pendingObject = null;
    }

    public void RotateObject()
    {
        pendingObject.transform.Rotate(Vector3.up, rotateAmount);
    }

    private float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }

    private void UpdateMaterials()
    {
        if (pendingObject == null)
            return;
        meshRenderers = pendingObject.GetComponentsInChildren<MeshRenderer>();
        if (canPlace)
        {
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.material = materials[0];
            }
            //pendingObject.GetComponentInChildren<MeshRenderer>().material = startingMaterial;
            return;
        }
        
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.material = materials[1];
        }

        //pendingObject.GetComponentInChildren<MeshRenderer>().material = materials[1];

    }

    [PunRPC]
    private void UpdateNewObjectMaterials(int viewId, int materialIndex)
    {
        GameObject newObject = PhotonView.Find(viewId).gameObject;
        MeshRenderer[] renderers = newObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material = materialsByPlayer[materialIndex];
        }
    }
}
