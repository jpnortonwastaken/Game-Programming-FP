using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool isFalling = false;
    float downSpeed = 0;
    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.name == "Player")
        {
            Invoke("StartRumble", 3);
            Invoke("StartFall", 3.5f);
            Destroy(gameObject, 4.5f);
        }

    }

    private void StartRumble()
    {
        animator.SetTrigger("rumbleTime");

    }
    private void StartFall()
    {
        animator.SetTrigger("falling");
    }

}
