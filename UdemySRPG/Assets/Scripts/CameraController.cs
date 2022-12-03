using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // TODO: set camera to pan when mouse is at the edges 
    // TODO: set camera drag to not have continuous movement 

    Vector3 mouseStart;
    [SerializeField] int camMoveSpeed = 10;
    int camSpeedDamp = 500;

    // [SerializeField] bool invertCamDrag = false;
    // int camDragValue = 1;
    void Update()
    {

        Vector3 mousePos = new Vector3(
            (-Screen.width / 2) + Input.mousePosition.x,
            0,
            (-Screen.height / 2) + Input.mousePosition.y);

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            mouseStart = mousePos;
        }
        if (Input.GetKey(KeyCode.Mouse2))
        {

            Vector3 direction = mouseStart - mousePos;
            Vector3 moveVector = transform.forward * -direction.x + transform.right * direction.z;
            transform.position -= moveVector / camSpeedDamp * camMoveSpeed * Time.deltaTime;

        }

        else
        {
            HandleCameraZoom();

        }

    }
    void HandleCameraZoom()
    {
        Vector3 zScroll = new Vector3(0, 30, 0);
        transform.localEulerAngles = zScroll;

        if (Input.mouseScrollDelta.y >= 0.1f || Input.mouseScrollDelta.y <= -0.1f)
        {
            zScroll = zScroll + new Vector3(0, 0, 40);
            transform.localEulerAngles = zScroll;
            Vector3 moveVector = transform.right * -(Input.mouseScrollDelta.y * 2f);
            transform.position += moveVector;
        }

    }
}
