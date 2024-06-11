using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLaunch : MonoBehaviour
{
    public float speed;
	public GameObject player;
    [Tooltip("From 0% to 100%")]
    public float accuracy;
    public float fireRate;
    public AudioClip shotSFX;
    public AudioClip hitSFX;
    public List<GameObject> trails;

    private Vector3 offset;
    private bool collided;
    private Rigidbody rb;
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent <Rigidbody> ();
		
		transform.LookAt(player.transform.position + new Vector3(0, 1, 0));

		if (shotSFX != null && GetComponent<AudioSource>()) {
			GetComponent<AudioSource> ().PlayOneShot (shotSFX);
		}

        Destroy(gameObject, 3);
    }

	void FixedUpdate () {	
			rb.velocity = gameObject.transform.forward * speed;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.LoseGame();
            }
        }
    }

}
