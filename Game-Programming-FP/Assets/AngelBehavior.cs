using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 10;
    public float attackDistance = 1f;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        float step = moveSpeed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        transform.LookAt(player);
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);

        if (distance > attackDistance)
        {
            animator.SetInteger("animMode", 0);
        }
        else if (distance <= attackDistance)
        {
            animator.SetInteger("animMode", 1);
        }
    }


}
