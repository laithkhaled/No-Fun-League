using UnityEngine;
using TMPro;
using System.Collections;

public class WinZone : MonoBehaviour
{
    public TMP_Text scoreText;
    public AudioSource scoreSound; 
    public AudioClip scoreClip; 

    private int score = 0;
    private bool hasScored = false;
    bool hasFootball = false;

    public GameManager gameManager;
    public TeamSwapper teamSwapper;
    public GameObject homeStartPos;
    public GameObject awayStartPos;

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
        while (true)
        {
            hasFootball = false;
            foreach (Transform child in player)
            {
                if (child.name == "Football(Clone)")
                {
                    hasFootball = true;
                    break;
                }
            }

            if (hasFootball && !hasScored)
            {
                score += 7;
                UpdateScoreUI();
                hasScored = true;

                // Play the score sound
                if(scoreSound && scoreClip)
                    scoreSound.PlayOneShot(scoreClip);

                // Stop all other coroutines before handling scoring
                StopAllCoroutines();
                HandleScore(player);
                yield break;
            }

            yield return null;
        }
    }

    void HandleScore(Transform player)
    {
        // Reset necessary gameplay elements
        gameManager.ResetDowns();
        teamSwapper.SwapTeams();

        // Teleport the Line of Scrimmage to the correct start position
        if (teamSwapper.isHomeTeamWithBall) // Assuming 'isHomeTeamWithBall' means home team just scored
        {
            gameManager.TeleportLineOfS_Away(awayStartPos.transform.position);
            hasScored = false;
        }
        else
        {
            gameManager.TeleportLineOfS(homeStartPos.transform.position);
            hasScored = false;
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }
}
