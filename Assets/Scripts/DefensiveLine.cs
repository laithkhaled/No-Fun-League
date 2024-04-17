using System.Collections;
using UnityEngine;

public class DefensiveLine : MonoBehaviour
{
    private DefensiveCoverage playerController;
    Rigidbody2D rb;

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the trigger collision is with an Offensive Lineman
        if (collider.gameObject.CompareTag("Defense"))
        {
            // Blocking animation
            animator.SetBool("isBlocking", true);
            DefensiveCoverage playerController = collider.GetComponent<DefensiveCoverage>();
            playerController.StopMovement(); // Call the method to handle getting tackled
        }
    }
}