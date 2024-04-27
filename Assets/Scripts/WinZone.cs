using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinZone : MonoBehaviour
{
    public GameObject team;
    private int score = 0;
    public TMP_Text scoreText;
    bool scored = false;

    void Start()
    {
        UpdateScoreUI();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called");
        
        if (other.CompareTag("Receiver"))
        {
            Debug.Log("Object is a receiver");
            
            // Check if the entering object has a child named "Football(Clone)"
            bool hasFootball = false;
            foreach (Transform child in other.transform)
            {
                if (child.name == "Football(Clone)")
                {
                    hasFootball = true;
                    break;
                }
            }

            if (hasFootball && !scored)
            {
                Debug.Log("Player has football and has not scored yet");
                
                // Increment the score when a player with the football enters the trigger area
                score += 7;
                UpdateScoreUI();
                scored = true; // Set scored to true to prevent multiple score increments in the same play
            }
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }

    // Will reset the scored flag when the player with football leaves the endzone
    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit called");
        
        if (other.transform.Find("Football(Clone)") != null)
        {
            Debug.Log("Football exited the trigger");
            
            scored = false; // Reset the scored flag
        }
    }
}
