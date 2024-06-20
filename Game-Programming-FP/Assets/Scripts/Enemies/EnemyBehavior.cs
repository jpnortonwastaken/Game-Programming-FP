using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
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
        else if (other.CompareTag("Attack"))
        {
            PlayerMovement.isKnockBacked = true;
            PlayerMovement.KnockBack();
            Destroy(gameObject);
        }
    }
}
