using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public GameObject projectile;
    public Transform launchPosition;
    public float launchForce;
    private float startingTimer;
    private float waitTime;
    public AnimationClip clip;
    private float currentTimer;

    public int ammoAmount;
    private List<GameObject> instatiatedProjectiles;

    // Start is called before the first frame update
    void Start()
    {
        instatiatedProjectiles= new List<GameObject>();
        startingTimer = clip.length;
        waitTime = startingTimer / 3;
        currentTimer = startingTimer;

        InstantiateProjectiles();
    }

    public void Launch()
    {
        GameObject projectile = GetPooledObject();
        if (projectile != null )
            {
                projectile.transform.position = launchPosition.position;
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
        
        for (int i = 0; i < ammoAmount; i++)
        {
            var proj = Instantiate(projectile, launchPosition.position, transform.rotation);
            proj.gameObject.SetActive(false);
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


}
