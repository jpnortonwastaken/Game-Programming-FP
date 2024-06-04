using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public bool readyToAttack;
    public static float knockback = 10;
    public GameObject slashEffect;
    public float attackCooldown = 1;
    public AudioClip hitSFX;

    // Start is called before the first frame update
    void Start()
    {
        readyToAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && !GameManager.gameEnded && readyToAttack)
        {
            readyToAttack = false;
            HitEnemy();
            Invoke(nameof(ResetAttack), attackCooldown);
        }
        

    }

    void HitEnemy()
    {
        GameObject attack = Instantiate(slashEffect, 
            transform.position + Camera.main.transform.forward, Camera.main.transform.rotation)
                as GameObject;
        attack.transform.SetParent(gameObject.transform);
        AudioSource.PlayClipAtPoint(hitSFX, transform.position);
    }

    public static void KnockBack()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(-1 * Camera.main.transform.forward * knockback, ForceMode.Impulse);
    }

    private void ResetAttack()
    {
        readyToAttack = true;
    }
}
