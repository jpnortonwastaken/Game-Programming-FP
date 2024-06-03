using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public Transform player;
    public float minDistance = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (PlayerAttack.hitEnemy && minDistance > distance)
        {
            Destroy(gameObject);
        }
        else
        {
            PlayerAttack.hitEnemy = false;
        }
    }
}
