using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float totalTime = 30f;
    private float currentTime;
    private bool isTimerRunning = false, isPlayRunning = false;
    private bool halftimeShown = false;

    public TMP_Text timerText;
    public TMP_Text homeScoreText;
    public TMP_Text awayScoreText;
    public TMP_Text quarterText;
    public int homeScore;
    public int awayScore;

    public GameObject flagCallWindow;
    public ThrowFlag throwFlagScript;
    public CharacterMovement characterMoveScript;

    public GameObject gameOverWindow;
    public GameObject winnerWindow;
    public GameObject halftimeWindow;

    void Start()
    {
        currentTime = totalTime;
        DisplayTime(totalTime);
        quarterText.text = "Quarter 1";
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F) && !isPlayRunning)
        {
            StartTimer();
            isPlayRunning = true;
            throwFlagScript.enabled = true;
            characterMoveScript.enabled = true;
        }

        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= totalTime / 2 && !halftimeShown)
            {
                StopTimer();
                halftimeShown = true;
                ShowHalftimeScreen();
                quarterText.text = "Quarter 2";
            }

            if (currentTime <= 0f)
            {
                StopTimer();
                homeScore = int.Parse(homeScoreText.text);
                awayScore = int.Parse(awayScoreText.text);
                if (homeScore > awayScore)
                {
                    PlayerWins();
                }
                else
                {
                    GameOver();
                }
            }

            DisplayTime(currentTime);
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void FlagLanded()
    {
        StopTimer();
        flagCallWindow.SetActive(true);
        characterMoveScript.enabled = false;
        throwFlagScript.enabled = false;
        isPlayRunning = false;
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameOver()
    {
        gameOverWindow.SetActive(true);
        // Player is not able to move and all velocity is stopped
        // Also cannot throw flags
        characterMoveScript.enabled = false;
        throwFlagScript.enabled = false;
        characterMoveScript.rb.velocity = Vector3.zero;
    }

    public void PlayerWins()
    {
        winnerWindow.SetActive(true);
        // Player is not able to move and all velocity is stopped
        // Also cannot throw flags
        characterMoveScript.enabled = false;
        throwFlagScript.enabled = false;
        characterMoveScript.rb.velocity = Vector3.zero;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        // Load next level or level map
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void ShowHalftimeScreen()
    {
        halftimeWindow.SetActive(true);
        // Player is not able to move and all velocity is stopped
        // Also cannot throw flags
        characterMoveScript.enabled = false;
        throwFlagScript.enabled = false;
        characterMoveScript.rb.velocity = Vector3.zero;
    }
}
