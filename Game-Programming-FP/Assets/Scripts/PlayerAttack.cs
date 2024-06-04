using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public static float knockback = 10;
    public GameObject slashEffect;
    float pitch = 0;

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

        }

    }

    void HitEnemy()
    {
        GameObject attack = Instantiate(slashEffect, transform.position + Camera.main.transform.forward, transform.rotation)
                as GameObject;
        attack.transform.SetParent(gameObject.transform);

        pitch = Camera.main.transform.rotation.eulerAngles.y;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        attack.transform.localRotation = Quaternion.Euler(pitch, 0, 0);

    }

    public static void KnockBack()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(-1 * Camera.main.transform.forward * knockback, ForceMode.Impulse);
    }
}
