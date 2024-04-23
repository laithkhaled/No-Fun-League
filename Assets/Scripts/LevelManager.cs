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
    public CharacterStamina staminaScript;

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
            //characterMoveScript.enabled = true;
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
        // Player is not able to move and all velocity is stopped
        // Also cannot throw flags
        //characterMoveScript.enabled = false;
        throwFlagScript.enabled = false;
        characterMoveScript.rb.velocity = Vector3.zero;

        // Play running flag is reset
        isPlayRunning = false;
    }

    public void EndPlay()
    {
        StopTimer();
        //characterMoveScript.enabled = false;
        throwFlagScript.enabled = false;
        characterMoveScript.rb.velocity = Vector3.zero;

        // Play running flag is reset
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

    public void LevelMapScene()
    {
        SceneManager.LoadScene("LevelMap");
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
        isPlayRunning = false;

        // Randomly choose the options for each button position
        RandomizeModButtons();
    }

    public void HalfTimeOver()
    {
        halftimeWindow.SetActive(false);
        characterMoveScript.enabled = true;
        throwFlagScript.enabled = true;
    }

    void RandomizeModButtons()
    {
        // Iterate through each button position
        for (int i = 1; i <= 3; i++)
        {
            // Randomly choose between the two options for the current button position
            bool option1 = Random.Range(0, 2) == 0;

            // Get buttons
            GameObject button1 = GameObject.Find("Modifier" + i);
            GameObject button2 = GameObject.Find("Modifier" + i + ".1");

            // Set the active button
            button1.SetActive(option1);
            // Set not active button
            button2.SetActive(!option1);
        }
    }

    // Modifiers
    public void IncreasePlayerSpeedDecreaseStamina()
    {
        // Increase player speed
        characterMoveScript.moveSpeed = 5;

        // Decrease player stamina
        staminaScript.totalStamina = 90;
    }

    public void IncreaseStaminaDecreasePlayerSpeed()
    {
        // Increase player stamina
        staminaScript.totalStamina = 120;

        // Decrease player speed
        characterMoveScript.moveSpeed = 3;
    }

    public void DecreaseSuspicionForFalseFouls()
    {
        // Decrease suspicion for false fouls
        SuspicionChecks.ModifySuspicionIncAmount(15f);
    }

    public void IncreaseSuspicionForTrueFouls()
    {
        // Increase suspicion for true fouls
        SuspicionChecks.ModifySuspicionDecAmount(20f);
    }

    public void AddTimeToTimer()
    {
        // Add time to the timer
        currentTime += 10;
        DisplayTime(currentTime);
    }

    public void AddExtraPointsToScore()
    {
        // Add extra points to the team's score
        homeScore += 7;
        // Update UI
        homeScoreText.text = homeScore.ToString();
    }

    public void ClearFlags()
    {
        GameObject[] flags = GameObject.FindGameObjectsWithTag("Flag");

        foreach (GameObject flag in flags)
        {
            Destroy(flag);
        }
    }
}
