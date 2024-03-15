using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinZone : MonoBehaviour
{
    public GameObject team;
    private int score = 0;
    private bool scored = false;
    public TMP_Text scoreText;

    void Start()
    {
        UpdateScoreUI();
    }

    void Update()
    {
        foreach (Transform child in team.transform)
        {
            // For testing 
            if (!child.gameObject.activeSelf)
            {
                continue;
            }

            // If player has football in correct endzone and no team has scored for this play
            // Scored check is to make sure score is updated once per play
            if (child.CompareTag("Player") && child.Find("Football") != null && !scored)
            {
                score += 7;
                scored = true;
                UpdateScoreUI();
            }
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // Will allow teams to score after play gets reset
    // When player with football leaves endzone, then scored is set to false to allow scoring again
    void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is tagged as "Player" and if it has the football child object
        if (other.CompareTag("Player") && other.transform.Find("Football") != null && scored)
        {
            // Reset the scored flag
            scored = false;
        }
    }
}
