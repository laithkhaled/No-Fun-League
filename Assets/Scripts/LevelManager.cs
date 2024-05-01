using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    static Slider suspicionSlider;
    float suspicion = 0f;
    float suspicionIncreaseRate = 5f;
    float maxSuspicion = 100f;
    static GameObject handle;
    static GameObject handle1;
    static GameObject handle2;
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
        ResumeTime();
        currentTime = totalTime;
        DisplayTime(totalTime);
        quarterText.text = "Quarter 1";

        suspicionSlider = GameObject.FindGameObjectWithTag("SuspicionMeter").GetComponent<Slider>();

        // Find the handles dynamically
        Transform handleTransform = suspicionSlider.transform.Find("Fill Area/Fill/Handle");
        Transform handle1Transform = suspicionSlider.transform.Find("Fill Area/Fill/Handle1");
        Transform handle2Transform = suspicionSlider.transform.Find("Fill Area/Fill/Handle2");

        if (handleTransform != null && handle1Transform != null && handle2Transform != null)
        {
            handle = handleTransform.gameObject;
            handle1 = handle1Transform.gameObject;
            handle2 = handle2Transform.gameObject;
        }
        else
        {
            Debug.LogError("One or more handles not found as great grandchildren of the Slider.");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F) && !isPlayRunning)
        {
            StartTimer();
            isPlayRunning = true;
            throwFlagScript.enabled = true;
        }

        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;

            // Increase suspicion meter every second while timer is running
            suspicion += suspicionIncreaseRate * Time.deltaTime;

            // Update the UI Slider value with the current suspicion level
            suspicionSlider.value = Mathf.Clamp01(suspicion / maxSuspicion);

            // Update suspicion slider
            UpdateSuspicionSliderValue();

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
        else
        {
            // Suspicion meter stops increasing when timer is stopped
            // No need to update the UI Slider in this case
        }
    }

    void UpdateSuspicionSliderValue()
    {

        suspicionSlider.value = suspicion;

        // Activate/deactivate handles based on suspicion level
        if (suspicion < 33)
        {
            handle.SetActive(true);
            handle1.SetActive(false);
            handle2.SetActive(false);
        }
        else if (suspicion >= 33 && suspicion < 66)
        {
            handle.SetActive(false);
            handle1.SetActive(true);
            handle2.SetActive(false);
        }
        else
        {
            handle.SetActive(false);
            handle1.SetActive(false);
            handle2.SetActive(true);
        }

        // Debug.Log("Suspicion: " + suspicion);
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
        Time.timeScale = 0f;
        flagCallWindow.SetActive(true);
        // Player cannot throw flags
        throwFlagScript.enabled = false;
        characterMoveScript.rb.velocity = Vector3.zero;

        // Play running flag is reset
        isPlayRunning = false;
    }

    public void EndPlay()
    {
        StopTimer();
        // Player cannot throw flags
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
        ClearFlags();
        ClearFouls();
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

    public void ClearFouls()
    {
        // Array to hold all foul tags
        string[] foulTags = { "PIRadius", "HoldingRadius", "RHRadius" };

        // Iterate through each foul tag
        foreach (string tag in foulTags)
        {
            // Find all game objects with the current foul tag
            GameObject[] fouls = GameObject.FindGameObjectsWithTag(tag);

            // Destroy each found game object
            foreach (GameObject foul in fouls)
            {
                Destroy(foul);
            }
        }
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }
}
