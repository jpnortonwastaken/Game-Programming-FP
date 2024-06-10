using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public bool readyToAttack;
    public GameObject slashEffect;
    public float attackCooldown = 1;
    public AudioClip hitSFX;
    public Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        readyToAttack = true;
        animator = GameObject.FindGameObjectWithTag("Player Model").GetComponent<Animator>();
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
        animator.SetTrigger("Attack");
    }

    private void ResetAttack()
    {
        readyToAttack = true;
    }
}
