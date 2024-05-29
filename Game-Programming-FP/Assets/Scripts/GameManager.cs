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
<<<<<<< HEAD
        replayButton.onClick.AddListener(ReplayGame);
        gameText.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(false);
    }

    void Update() {
        if (GameObject.FindGameObjectWithTag("Player").transform.position.y < -5) {
            LoseGame();
        }
    }

    public void ShowScreen() {
        gameText.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

=======
        gameText.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(false);
        replayButton.onClick.AddListener(ReplayGame);
    }
>>>>>>> main
    public void WinGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            gameText.text = "You Win!";
<<<<<<< HEAD
            ShowScreen();
=======
            gameText.gameObject.SetActive(true);
            replayButton.gameObject.SetActive(true);
>>>>>>> main
        }
    }

    public void LoseGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            gameText.text = "You Lose!";
<<<<<<< HEAD
            ShowScreen();
=======
            gameText.gameObject.SetActive(true);
            replayButton.gameObject.SetActive(true);
>>>>>>> main
        }
    }
    public void ReplayGame()
    {
<<<<<<< HEAD
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
=======
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); /
>>>>>>> main
    }
}

