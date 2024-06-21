using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalObjectBehavior : MonoBehaviour
{
    public AudioClip winSound;
    // Start is called before the first frame update
    void Start()
    {
        
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
                if(!gameManager.IsGameOver()){
                      AudioSource.PlayClipAtPoint(winSound, transform.position);
                    gameManager.WinGame();
                }
              
            }
        }
    }
    
}
