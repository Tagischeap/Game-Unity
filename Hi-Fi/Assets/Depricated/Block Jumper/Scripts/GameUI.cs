using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public GameObject endScreen;
    public TextMeshProUGUI endScreenHeader;
    public TextMeshProUGUI endScreenScoreText;

    public GameObject pauseScreen;

    // instance
    public static GameUI instance;

    void Awake ()
    {
        instance = this;
    }

    void Start ()
    {
        UpdateScoreText();
    }

    // updates the player's score text
    public void UpdateScoreText ()
    {
        scoreText.text = "Score: " + GameManager1.instance.score;
    }

    // sets the end screen for a win or game over
    public void SetEndScreen (bool hasWon)
    {
        endScreen.SetActive(true);

        endScreenScoreText.text = "<b>Score</b>\n" + GameManager1.instance.score;

        if(hasWon)
        {
            endScreenHeader.text = "You Win";
            endScreenHeader.color = Color.green;
        }
        else
        {
            endScreenHeader.text = "Game Over";
            endScreenHeader.color = Color.red;
        }
    }

    // called when the "Restart" button is pressed
    public void OnRestartButton ()
    {
        GameManager1.instance.ResetScore();
        GameManager1.instance.TogglePauseGame();
        SceneManager.LoadScene(1);
    }

    // called when the "Menu" button is pressed
    public void OnMenuButton ()
    {
        if(GameManager1.instance.paused)
            GameManager1.instance.TogglePauseGame();

        SceneManager.LoadScene(0);
    }

    // called when the game is paused or un-paused
    public void TogglePauseScreen (bool paused)
    {
        pauseScreen.SetActive(paused);
    }

    // called when the "Resume" button is pressed
    public void OnResumeButton ()
    {
        GameManager1.instance.TogglePauseGame();
    }
}