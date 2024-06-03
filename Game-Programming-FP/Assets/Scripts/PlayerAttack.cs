using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static bool hitEnemy = false;
    public float knockback = 5;

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
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            if (PlayerMovement.grounded)
            {
                rb.AddForce(-1 * Camera.main.transform.forward * knockback, ForceMode.Impulse);
            }
        }

    }

    void HitEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity) 
            && hit.collider.CompareTag("Enemy"))
        {
            hitEnemy = true;
        }
    }
}
