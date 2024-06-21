using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCloud : MonoBehaviour
{
    public float speed = 1;
    public float distance = 4;
    Vector3 startPos;
    Vector3 previousPos;

    private bool playerOnCloud = false;
    private Transform playerTransform;

    void Start()
    {
        startPos = transform.position;
        previousPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.x = startPos.x + Mathf.Sin(Time.time * speed) * distance;

        transform.position = newPos;

        if (playerOnCloud && !IsPlayerMoving()) 
        {
            playerTransform.position = new Vector3(
                playerTransform.position.x + (transform.position.x - previousPos.x),
                playerTransform.position.y,
                playerTransform.position.z
            );
        }

        previousPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerOnCloud = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnCloud = false;
            playerTransform = null;
        }
    }

    private bool IsPlayerMoving()
    {
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }
}