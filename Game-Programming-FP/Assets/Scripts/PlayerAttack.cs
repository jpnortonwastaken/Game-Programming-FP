using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float knockback = 5;
    public GameObject slashEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            HitEnemy();
            KnockBack();

        }

    }

    void HitEnemy()
    {
        GameObject attack = Instantiate(slashEffect, transform.position + transform.forward, transform.rotation)
                as GameObject;
        attack.transform.SetParent(gameObject.transform);

    }

    void KnockBack()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (EnemyBehavior.hitEnemy && !PlayerMovement.grounded)
        {
            knockback = 6;
        }
        else if (EnemyBehavior.hitEnemy)
        {
            knockback = 5;
        }
        else
        {
            knockback = 0;
        }
        rb.AddForce(-1 * Camera.main.transform.forward * knockback, ForceMode.Impulse);
    }
}
