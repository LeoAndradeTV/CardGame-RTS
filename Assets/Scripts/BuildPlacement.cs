using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildPlacement : MonoBehaviour
{
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

    public bool canPlace = true;
    public bool mouseOverUIElement => EventSystem.current.IsPointerOverGameObject();


    private void Update()
    {
        if (pendingObject != null)
        {
            Debug.Log("We got here");
            pendingObject.transform.position = new Vector3(
                RoundToNearestGrid(pos.x),
                RoundToNearestGrid(pos.y),
                RoundToNearestGrid(pos.z));

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
        pendingObject = Instantiate(objects[index], pos, transform.rotation);
        startingMaterial = pendingObject.GetComponentInChildren<MeshRenderer>().material;
        pendingObjectBuildingData = buildingData;
    }

    public void PlaceObject(BuildType building)
    {
        meshRenderers = pendingObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.material = startingMaterial;
        }
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
}
