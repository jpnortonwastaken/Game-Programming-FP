using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameEnded = false;
    public Text gameText;
     public Button replayButton;
    void Start(){
        gameText.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(false);
        replayButton.onClick.AddListener(ReplayGame);
    }
    public void WinGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            gameText.text = "You Win!";
            gameText.gameObject.SetActive(true);
            replayButton.gameObject.SetActive(true);
        }
    }

    public void LoseGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            gameText.text = "You Lose!";
            gameText.gameObject.SetActive(true);
            replayButton.gameObject.SetActive(true);
        }
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); /
    }
}

