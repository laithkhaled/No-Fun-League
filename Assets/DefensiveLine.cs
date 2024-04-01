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
        // Check if the trigger collision is with an Offensive Lineman
        if (collider.gameObject.CompareTag("Defense"))
        {
            DefensiveCoverage playerController = collider.GetComponent<DefensiveCoverage>();
            playerController.StopMovement(); // Call the method to handle getting tackled
        }
    }
}