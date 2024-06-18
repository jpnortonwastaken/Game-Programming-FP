using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotController : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.08f;
    public float ySmoothSpeed = 0.25f;
    public Vector3 offset = new Vector3(0, 2, 0);

    void Start()
    {
        transform.SetParent(null);
        
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.y = Mathf.Lerp(transform.position.y, desiredPosition.y, ySmoothSpeed);

        transform.position = smoothedPosition;
    }
}
