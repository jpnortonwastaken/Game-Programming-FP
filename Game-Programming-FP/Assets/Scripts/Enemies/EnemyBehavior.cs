using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    
    public static bool isKnockBacked;
    public float knockBackCooldown = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        isKnockBacked = false;
    }

    // Update is called once per frame
    void Update()
    {

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
        else if (other.CompareTag("Attack"))
        {
            isKnockBacked = true;
            PlayerAttack.KnockBack();
            Destroy(gameObject);
            isKnockBacked = false;
            //Invoke(nameof(ResetKnockBack), knockBackCooldown);
        }
    }

/*
    private void ResetKnockBack()   
    {
        isKnockBacked = false;
    } */
}
