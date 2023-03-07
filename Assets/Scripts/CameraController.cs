using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public bool canMoveCamera;

    private Player player;

    private float speed = 150f;
    private float currentZoom = 277f;
    private float zoomSpeed = 40f;
    private float minZoom = 25f;
    private float maxZoom = 277f;
    private float minVertical = 110f;
    private float maxVertical = 375f;
    private float minHorizontal = -110f;
    private float maxHorizontal = 110f;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        player = PhotonNetwork.LocalPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMoveCamera)
        {
            currentZoom = UIHandler.instance.lastCameraPositionOnTable.y;
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Support.GetPlayerRoomId(player) == 0)
        {
            transform.position = new Vector3(transform.position.x + horizontal * speed * Time.deltaTime, currentZoom, transform.position.z + vertical * speed * Time.deltaTime);
        }
        else if (Support.GetPlayerRoomId(player) == 1)
        {
            transform.position = new Vector3(transform.position.x - vertical * speed * Time.deltaTime, currentZoom, transform.position.z + horizontal * speed * Time.deltaTime);
        }
        else if (Support.GetPlayerRoomId(player) == 2)
        {
            transform.position = new Vector3(transform.position.x - horizontal * speed * Time.deltaTime, currentZoom, transform.position.z - vertical * speed * Time.deltaTime);
        }
        else if (Support.GetPlayerRoomId(player) == 3)
        {
            transform.position = new Vector3(transform.position.x + vertical * speed * Time.deltaTime, currentZoom, transform.position.z - horizontal * speed * Time.deltaTime);

        }

        SetCameraInBound();

        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

    }

    private void SetCameraInBound()
    {
        if (transform.position.x < minHorizontal)
        {
            transform.position = new Vector3(minHorizontal, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > maxHorizontal)
        {
            transform.position = new Vector3(maxHorizontal, transform.position.y, transform.position.z);
        }
        else if (transform.position.z < minVertical)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minVertical);
        }
        else if (transform.position.z > maxVertical)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxVertical);
        }

    }
}
