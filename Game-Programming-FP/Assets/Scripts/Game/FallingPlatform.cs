using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool isFalling = false;
    float downSpeed = 0;
    public Transform currentTransform;
    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.name == "Player")
        {
            Invoke("StartFalling", 3);
            Destroy(gameObject, 5);
        }

    }

    private void Update()
    {
        if (isFalling)
        {
            downSpeed = 0.5f;
            transform.position = new Vector3(transform.position.x,
                transform.position.y - downSpeed,
                transform.position.z);
        }
    }

    private void StartFalling()
    {
        isFalling = true;
    }
}
