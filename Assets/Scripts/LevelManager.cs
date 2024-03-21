using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public float totalTime = 30f;
    private float currentTime;
    private bool isTimerRunning = false, isPlayRunning = false;
    public TMP_Text timerText;

    public GameObject flagCallWindow;
    public ThrowFlag throwFlagScript;
    public CharacterMovement characterMoveScript;

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
}
