using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    public AudioClip punchSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                AudioSource.PlayClipAtPoint(punchSound, transform.position);
                gameManager.TakeHit();
            }
        }
        else if (other.CompareTag("Attack"))
        {
            PlayerMovement.isKnockBacked = true;
            PlayerMovement.KnockBack();
            Destroy(gameObject);
        }
    }
}
