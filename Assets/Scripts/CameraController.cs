using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public bool canMoveCamera;

    private float speed = 150f;
    private float currentZoom = 277f;
    private float zoomSpeed = 40f;
    private float minZoom = 75f;
    private float maxZoom = 277f;
    private float minVertical = 128f;
    private float maxVertical = 350f;
    private float minHorizontal = -90f;
    private float maxHorizontal = 90f;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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

        transform.position = new Vector3(transform.position.x + horizontal * speed * Time.deltaTime, currentZoom, transform.position.z + vertical * speed * Time.deltaTime);

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
