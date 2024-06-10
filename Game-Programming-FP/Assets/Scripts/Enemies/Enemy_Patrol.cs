using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;
    public float waitTime = 0.5f;

    private Transform target;
    private bool isWaiting = false;

    void Start()
    {
        target = pointA;
        transform.RotateAround(transform.position, transform.up, 180);
    }

    void Update()
    {
        if (!isWaiting)
        {
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                StartCoroutine(WaitAtPoint());
                transform.RotateAround(transform.position, transform.up, 180);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }

    }

    IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        target = target == pointA ? pointB : pointA;
        isWaiting = false;
    }
}
