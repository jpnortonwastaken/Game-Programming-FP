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
    public Text timerText;
    public Button replayButton;
    private float elapsedTime;
    public int starCount;
    public Image star1;
    public Image star2;
    public Image star3;
    public GameObject endPanel;
    private CoinImageHandler star1Script;
    private CoinImageHandler star2Script;
    private CoinImageHandler star3Script;
    public float timeRemaining = 100; 
    public bool timerIsRunning = false;
    private int hearts;

    GameObject heart1;
    GameObject heart2;
    GameObject heart3;
    public Sprite emptyHeart;
    GameObject player;
    public AudioClip loseSound;
    public AudioClip hitSound;


    
    void ChangeHeartImage(GameObject heart){
        Image img1 = heart.GetComponent<Image>();
        if (img1 != null)
        {
            img1.sprite = emptyHeart;
        }
    }

    void Start(){
        timerIsRunning=true;
        timerText.text = timeRemaining.ToString();
        starCount = 0;
        elapsedTime = 0;
        gameEnded = false;
        Time.timeScale = 1f;
        replayButton.onClick.AddListener(ReplayGame);
        gameText.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(false);
        star1Script = star1.GetComponent<CoinImageHandler>();
        star2Script = star2.GetComponent<CoinImageHandler>();
        star3Script = star3.GetComponent<CoinImageHandler>();
        hearts = 3;
       heart1 = GameObject.FindGameObjectWithTag("heart1");
       heart2 = GameObject.FindGameObjectWithTag("heart2");
       heart3 = GameObject.FindGameObjectWithTag("heart3");
       player = GameObject.FindGameObjectWithTag("Player");

    }


    void Update() {
        if(!gameEnded){
            elapsedTime += Time.deltaTime;
        }
        if (player.transform.position.y < -5) {
            LoseGame();
        }
         if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                LoseGame();
            }
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
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ShowScreen(string input) {
        if(input == "win"){
            gameText.color = Color.white;
            endPanel.SetActive(true);
            gameText.gameObject.SetActive(true);
            replayButton.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }else{
            gameText.color = Color.red;
            endPanel.SetActive(true);
            gameText.gameObject.SetActive(true);
            replayButton.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void WinGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            gameText.text = "You Win!";
            Invoke("LoadNextLevel",1);
        }
    }
    public void TakeHit(){
        if(hearts==3){
            ChangeHeartImage(heart3);
        }else if ( hearts ==2){
            ChangeHeartImage(heart2);
        }else if ( hearts == 1){
            ChangeHeartImage(heart1);
        }
        AudioSource.PlayClipAtPoint(hitSound, player.transform.position);
        hearts--;
        if(hearts<=0){
            LoseGame();
        }

    }

    public void LoseGame()
    {
        player.GetComponent<PlayerMovement>().Death();

        if (!gameEnded)
        {
            AudioSource.PlayClipAtPoint(loseSound, player.transform.position);
            gameEnded = true;
            //Time.timeScale = 0f;
            gameText.text = "You Lose!";
            ShowScreen("lose");
        }
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Start();
    }
    void LoadNextLevel(){
        if(!finalGame){
            SceneManager.LoadScene(nextLevel);
        }else{
            ShowScreen("win");
        }
    }
    public bool IsGameOver(){
        return gameEnded;
    }

}

