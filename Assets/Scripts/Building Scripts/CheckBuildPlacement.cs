using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBuildPlacement : MonoBehaviour
{
    BuildPlacement buildPlacement;

    private float buildZMax = 175f;

    void Start()
    {
        buildPlacement = GameObject.Find("BuildPlacement").GetComponent<BuildPlacement>();
    }

    private void FixedUpdate()
    {
        buildPlacement.canPlace = buildPlacement.pos.z < buildZMax;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("BuiltObject"))
        {
            buildPlacement.canPlace = false;
        } else if (other.gameObject.CompareTag("Vegetation")) 
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
