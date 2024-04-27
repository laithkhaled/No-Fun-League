using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinZone : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;
    private bool hasScored = false; // Flag to track if the score has already been incremented

    void Start()
    {
        UpdateScoreUI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Receiver"))
        {
            StartCoroutine(CheckForFootball(other.transform));
        }
    }

    IEnumerator CheckForFootball(Transform player)
    {
        // Continuously check for the presence of the football child
        while (true)
        {
            bool hasFootball = false;
            foreach (Transform child in player)
            {
                if (child.name == "Football(Clone)")
                {
                    hasFootball = true;
                    break;
                }
            }

            if (hasFootball && !hasScored) // Check if the player has the football and hasn't scored yet
            {
                // Increment the score when a player with the football enters the trigger area
                score += 7;
                UpdateScoreUI();
                hasScored = true; // Set the flag to indicate that the score has been incremented
            }

            yield return null; // Wait for the next frame to check again
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }
}
