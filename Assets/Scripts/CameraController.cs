using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public bool canMoveCamera;

    private float speed = 40f;
    private float currentZoom = 277f;
    private float zoomSpeed = 40f;
    private float minZoom = 150f;
    private float maxZoom = 277f;

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
        if (canMoveCamera)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            //transform.position += new Vector3(horizontal, currentZoom, vertical) * speed * Time.deltaTime;

            transform.position = new Vector3(transform.position.x + horizontal * speed * Time.deltaTime, currentZoom, transform.position.z + vertical * speed * Time.deltaTime);

            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        }
    }
}
