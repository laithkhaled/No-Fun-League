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
    public TMP_Text timerText;

    public GameObject flagCallWindow;
    public ThrowFlag throwFlagScript;
    public CharacterMovement characterMoveScript;

    public GameObject gameOverWindow;
    public GameObject winnerWindow;

    void Start()
    {
        currentTime = totalTime;
        // Update timer on start
        DisplayTime(totalTime);
    }

    void Update()
    {
        // If player presses f, timer starts
        if (Input.GetKey(KeyCode.F) && !isPlayRunning)
        {
            StartTimer();
            isPlayRunning = true;
            // Can throw flags and move
            throwFlagScript.enabled = true;
            characterMoveScript.enabled = true;
        }

        // Update the timer if it is running
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;

            // Stop timer if time runs out
            if (currentTime <= 0f)
            {
                StopTimer();
                // Retrieve scores from winzone script 
                // If home score is higher then home wins
                    // Win level
                // Else lose level
            }

            // Update timer text
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

    // Called when flag lands
    public void FlagLanded()
    {
        StopTimer();
        flagCallWindow.SetActive(true);
        // Turn off player movmeent and flag throwing
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

    // Game Over
    public void GameOver()
    {
        gameOverWindow.SetActive(true);
        characterMoveScript.enabled = false;
        throwFlagScript.enabled = false;
    }

    public void PlayerWins()
    {
        winnerWindow.SetActive(true);
        characterMoveScript.enabled = false;
        throwFlagScript.enabled = false;
    }

    // Reset scene
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        // Load next level or level map
    }
}
