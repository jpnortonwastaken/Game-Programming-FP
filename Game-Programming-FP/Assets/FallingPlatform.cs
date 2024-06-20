using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool isFalling = false;
    float downSpeed = 0;
    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.name == "Player")
        {
            Invoke("StartFalling", 3);
        }

    }

    private void Update()
    {
        if (isFalling)
        {
            downSpeed = 4;
            transform.position = new Vector3(transform.position.x,
                transform.position.y - downSpeed,
                transform.position.x);
        }
    }

    private void StartFalling()
    {
        isFalling = true;
    }
}
