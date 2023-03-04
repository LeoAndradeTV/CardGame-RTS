using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Net;

public class LaunchProjectile : MonoBehaviour
{
    private PhotonView photonView;

    public GameObject projectile;
    public Transform launchPosition;
    public float launchForce;
    public AnimationClip clip;

    public int ammoAmount;
    private List<GameObject> instatiatedProjectiles;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        instatiatedProjectiles = new List<GameObject>();

        InstantiateProjectiles();
    }

    public void Launch()
    {
        GameObject projectile = GetPooledObject();
        if (projectile != null )
            {
                projectile.transform.position = launchPosition.position;
                projectile.transform.rotation = launchPosition.rotation;
                projectile.SetActive(true);
                ApplyForce(projectile);
            }
    }

    public void ApplyForce(GameObject projectile)
    {
        if (projectile.GetComponent<Rigidbody>() != null)
        {
            projectile.GetComponent<Rigidbody>().AddForce((transform.forward + Vector3.up) * launchForce, ForceMode.Impulse);
        }
        
    }

    public void InstantiateProjectiles()
    {
        if (instatiatedProjectiles.Count != 0) { return; }
        
        for (int i = 0; i < ammoAmount; i++)
        {
            var proj = Instantiate(projectile, transform.position, transform.rotation);
            proj.SetActive(false);
            instatiatedProjectiles.Add(proj);

        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < instatiatedProjectiles.Count; i++)
        {
            if (!instatiatedProjectiles[i].activeInHierarchy)
            {
                return instatiatedProjectiles[i];
            }
        }

        return null;
    }

    public void DestroyInstantiatedProjectiles()
    {
        int rounds = instatiatedProjectiles.Count;
        for (int i = 0; i < rounds; i++)
        {
            Destroy(instatiatedProjectiles[i]);
        }
    }

    private void OnEnable()
    {
        Actions.OnTurnEnded += DestroyInstantiatedProjectiles;
    }

    private void OnDisable()
    {
        Actions.OnTurnEnded -= DestroyInstantiatedProjectiles;
    }
}
