using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingBuildInfo
{
    public Vector3 position;
    public Quaternion rotation;
    public Material material;

    public StartingBuildInfo(Vector3 pos, Quaternion rot, Material mat)
    {
        position = pos;
        rotation = rot;
        material = mat;
    }
}
