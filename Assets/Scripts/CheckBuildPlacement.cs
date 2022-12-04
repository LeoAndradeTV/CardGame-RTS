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

    private void Update()
    {
        if (buildPlacement.pos.z > buildZMax)
        {
            buildPlacement.canPlace = false;
            return;
        }
        buildPlacement.canPlace = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("BuiltObject"))
        {
            buildPlacement.canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BuiltObject"))
        {
            buildPlacement.canPlace = true;
        }
    }
}
