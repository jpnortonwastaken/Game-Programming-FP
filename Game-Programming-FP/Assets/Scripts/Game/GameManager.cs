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
    public Button replayButton;
    private float elapsedTime;
    public float secondStarCount = 60f;
    public float thirdStarCount = 30f;
    public int starCount;
    public Image star1;
    public Image star2;
    public Image star3;
    private CoinImageHandler star1Script;
    private CoinImageHandler star2Script;
    private CoinImageHandler star3Script;
    

    void Start(){
        starCount = 0;
        elapsedTime = 0;
        gameEnded = false;
        replayButton.onClick.AddListener(ReplayGame);
        gameText.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(false);
        star1Script = star1.GetComponent<CoinImageHandler>();
        star2Script = star2.GetComponent<CoinImageHandler>();
        star3Script = star3.GetComponent<CoinImageHandler>();

    }

    void Update() {
        if(!gameEnded){
            elapsedTime += Time.deltaTime;
        }
        if (GameObject.FindGameObjectWithTag("Player").transform.position.y < -5) {
            LoseGame();
        }
    }
    public void HandleAddStar(){
        if(starCount==0){
            star1.gameObject.SetActive(true);
            star1Script.FadeOut(true);
        }else if(starCount == 1){
            star2.gameObject.SetActive(true);
            star2Script.FadeOut(true);
        }else if (starCount == 2){
            star3.gameObject.SetActive(true);
            star3Script.FadeOut(true); 
        }
        starCount++;
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
            LoadNextLevel();
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

