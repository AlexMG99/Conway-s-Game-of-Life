using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 0.15f;
    public float movementSpeed = 0.5f;
    public int maxZoomIn = 5;
    public int maxZoomOut = 40;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadPlus) && Camera.main.orthographicSize > maxZoomIn)
        {
            Camera.main.orthographicSize -= zoomSpeed * Time.deltaTime;

            if (maxZoomIn >= Camera.main.orthographicSize)
                Camera.main.orthographicSize = maxZoomIn;
        }
        else if (Input.GetKey(KeyCode.KeypadMinus) && Camera.main.orthographicSize < maxZoomOut)
        {
            Camera.main.orthographicSize += zoomSpeed * Time.deltaTime;

            if (maxZoomOut <= Camera.main.orthographicSize)
                Camera.main.orthographicSize = maxZoomOut;
        }

        Camera.main.orthographicSize -= zoomSpeed * Input.mouseScrollDelta.y * Time.deltaTime;

        float posX = Camera.main.transform.position.x;
        float posY = Camera.main.transform.position.y;
        float posZ = Camera.main.transform.position.z;

        if (Input.GetKey(KeyCode.W))
        {
            posY += movementSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            posY -= movementSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            posX -= movementSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            posX += movementSpeed * Time.deltaTime;
        }

        Camera.main.transform.position = new Vector3(posX, posY, posZ);
    }
}
