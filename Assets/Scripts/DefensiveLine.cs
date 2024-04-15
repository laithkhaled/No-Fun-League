using System.Collections;
using UnityEngine;

public class DefensiveLine : MonoBehaviour
{
    private DefensiveCoverage playerController;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        int offensiveLinemanStrength = Random.Range(1, 21);
        int defensiveLinemanStrength = Random.Range(1, 21);

        if (collider.gameObject.CompareTag("Defense") && offensiveLinemanStrength >= defensiveLinemanStrength)
        {
            DefensiveCoverage playerController = collider.gameObject.GetComponent<DefensiveCoverage>();
            if (playerController != null)
            {
                playerController.StopMovement(); 
            }
        }
    }
}