using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerBody;


    void Start()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float cameraYaw = Camera.main.transform.eulerAngles.y;
        Quaternion newRotation = Quaternion.Euler(0f, cameraYaw, 0f);
        playerBody.transform.rotation = Quaternion.Lerp(playerBody.transform.rotation, newRotation, Time.deltaTime * 100);
    }
}
