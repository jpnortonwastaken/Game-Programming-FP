using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    public float speed = 2.0f;
    public float moveSpeed = 5.0f;
    GameManager gameManager;
    public AudioClip pickupSound; 
    
    void Start()
    {
       gameManager = FindObjectOfType<GameManager>();
    }
 
    void Update()
    {
        transform.Rotate(.25f,0f,.25f, Space.Self);
    }  
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            gameManager.HandleAddStar();
            Destroy(gameObject);
        }
    }

}
