using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCloud : MonoBehaviour
{
    public float speed = 1;
    public float distance = 4;
    Vector3 startPos;
    Vector3 previousPos;
    private Rigidbody rb;
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

        if (rb != null)
        {
            Vector3 platformVector = transform.position - previousPos;
            rb.velocity += platformVector * speed;
        }

        previousPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rb = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rb = null;
        }
    }
}
