using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUnit : MonoBehaviour
{
    private float lifetime = 0;

    [SerializeField] float minimumLifetime;
    [SerializeField] float maximumLifetime;


    // Start is called before the first frame update
    void OnEnable()
    {
        lifetime = Random.Range(minimumLifetime, maximumLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime <= Mathf.Epsilon) { PhotonNetwork.Destroy(gameObject); return; }

        lifetime -= Time.deltaTime;
    }
}
