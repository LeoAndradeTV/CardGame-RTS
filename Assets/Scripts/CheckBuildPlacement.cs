using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBuildPlacement : MonoBehaviour
{
    BuildPlacement buildPlacement;

    void Start()
    {
        buildPlacement = GameObject.Find("BuildPlacement").GetComponent<BuildPlacement>();
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
