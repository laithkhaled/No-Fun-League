using System.Collections;
using UnityEngine;

public class DefensiveLine : MonoBehaviour
{
    private DefensiveCoverage playerController;
    Rigidbody2D rb;

    public Animator animator;
    //public GameObject holdingRadiusPrefab;
    public float duration = 8f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /*void OnTriggerEnter2D(Collider2D collider)
    {
        // Generate a random integer between 1 and 2
        int randomChoice = Random.Range(1, 3);

        // Check if the random integer equals 1 (50% chance)
        if (randomChoice == 1 && collider.gameObject.CompareTag("Defense"))
        {
            // Blocking animation
            animator.SetBool("isBlocking", true);

            DefensiveCoverage playerController = collider.GetComponent<DefensiveCoverage>();
            playerController.StopMovement(); // Call the method to handle getting tackled
        }
        else
        {
            // Instantiate the RHRadiusPrefab at the current position of this GameObject
            GameObject holdingRadius = Instantiate(holdingRadiusPrefab, transform.position, Quaternion.identity);

            // Destroy the radius after a set time
            Destroy(holdingRadius, duration);
        }
    }
    */
}
