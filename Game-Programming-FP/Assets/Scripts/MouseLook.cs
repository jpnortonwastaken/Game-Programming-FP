using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerBody;
    public float mouseSensitivity = 250;
    float yaw = 0;
    float pitch = 0;
    public float distance = 8;

    void Start()
    {
        playerBody = transform.parent.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //yaw
        yaw += moveX;
        playerBody.Rotate(Vector3.up * moveX); //this should move player body

        //pitch
        pitch -= moveY;

        //direction vector
        Vector3 direction = new Vector3(0, 0, -distance);

        pitch = Mathf.Clamp(pitch, -89.9f, 89.9f);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = playerBody.position + rotation * direction;

        transform.LookAt(playerBody.position);
    }
}
