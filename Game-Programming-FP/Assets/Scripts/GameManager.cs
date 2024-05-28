using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameEnded = false;
    public Text gameText;
    void Start(){
        gameText.gameObject.SetActive(false);
    }
    public void WinGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            Debug.Log("You Win!");
            // Add your win logic here, such as loading a win scene or showing a win UI
            // For demonstration, we'll just reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LoseGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            gameText.text = "You Win!";
            gameText.gameObject.SetActive(true);
            // Add your lose logic here, such as loading a lose scene or showing a lose UI
            // For demonstration, we'll just reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

