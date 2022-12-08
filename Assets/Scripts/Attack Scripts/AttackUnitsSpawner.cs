using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUnitsSpawner : MonoBehaviour
{
    public GameObject[] unitPrefabs;
    private List<Vector3> spawnPositions = new List<Vector3>();
    private float maxX = 40;
    private float maxZ = 20;
    float x = 0, y = 0, z = 0;

    public void SpawnUnits(int archers, int swordsmen, int sieges)
    {
        spawnPositions.Clear();
        buildSpawnPositions(archers + swordsmen + sieges);
        SpawnSieges(sieges);
        SpawnArchers(archers);
        SpawnSwordsmen(swordsmen);
    }


    private void InstantiateUnit(int unitNumber)
    {
        Instantiate(unitPrefabs[unitNumber], transform.position + spawnPositions[0], Quaternion.identity);
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
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPos = new Vector3(x, y, z);
            if (x < maxX)
            {
                x += 2;
            } else
            {
                z += 3;
                x = 0;
            }
            if (z > maxZ)
            {
                Debug.Log("Most units deployed");
            }
            spawnPositions.Add(spawnPos);
        }
    }
}
