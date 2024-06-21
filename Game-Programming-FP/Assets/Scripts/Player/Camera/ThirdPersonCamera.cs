using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform pivot;
    public Vector3 offset = new Vector3(0, 2, -6);
    public float sensitivity = 300f;
    public float minYAngle = -35f;
    public float maxYAngle = 60f;

    private float yaw = 0f;
    private float pitch = 25f;

    void Start()
    {
        sensitivity = PlayerPrefs.GetFloat("sensitivity", 200f);

        transform.SetParent(null);
        
        if (pivot == null)
        {
            pivot = GameObject.Find("CameraPivot").transform;
        }

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = 25f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, minYAngle, maxYAngle);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = pivot.position + rotation * offset;

        transform.position = desiredPosition;
        transform.LookAt(pivot.position);
    }
}
