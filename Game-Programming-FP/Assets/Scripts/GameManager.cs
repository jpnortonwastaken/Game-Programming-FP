using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool finalGame;
    public string nextLevel;
    public static bool gameEnded = false;
    public Text gameText;
    public Text starText;
    public Button replayButton;
    private float elapsedTime;
    public float secondStarCount = 60f;
    public float thirdStarCount = 30f;

    void Start(){
        elapsedTime = 0;
        gameEnded = false;
        replayButton.onClick.AddListener(ReplayGame);
        gameText.gameObject.SetActive(false);
        starText.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(false);
    }

    void Update() {
        if(!gameEnded){
            elapsedTime += Time.deltaTime;
        }
        if (GameObject.FindGameObjectWithTag("Player").transform.position.y < -5) {
            LoseGame();
        }
    }
    public string GetStarCount(){
        int starCount = 1;
        if(elapsedTime<=secondStarCount){
            starCount+=1;
        }
        if (elapsedTime<= thirdStarCount){
            starCount+=1;
        }
        return new string('★', starCount);
    }


    public void ShowScreen() {
        gameText.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void WinGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            gameText.text = "You Win!";
            starText.text = GetStarCount();
            starText.gameObject.SetActive(true);
            Invoke("LoadNextLevel", 2);
        }
    }

    public void LoseGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            gameText.text = "You Lose!";
            ShowScreen();
        }
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void LoadNextLevel(){
        if(!finalGame){
            SceneManager.LoadScene(nextLevel);
        }else{
            ShowScreen();
        }
    }
}

