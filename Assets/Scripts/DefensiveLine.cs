using System.Collections;
using UnityEngine;

public class DefensiveLine : MonoBehaviour
{
    private DefensiveCoverage playerController;
    private bool radiusSpawned = false;
    Rigidbody2D rb;

    public Animator animator;
    public GameObject holdingRadiusPrefab;
    float duration = 6f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // 50% chance to block
        int randomChoice = Random.Range(1, 3);
        // Rare chance for holding foul
        int randomChoice2 = Random.Range(1, 20);

        if (!radiusSpawned)
        {
            if (randomChoice == 1 && collider.gameObject.CompareTag("Defense"))
            {
                // Blocking animation
                animator.SetBool("isBlocking", true);

                DefensiveCoverage playerController = collider.GetComponent<DefensiveCoverage>();
                playerController.StopMovement(); // Call the method to handle getting tackled
            }
            else if (collider.gameObject.CompareTag("Defense") && randomChoice2 == 2)
            {
                // Instantiate the RHRadiusPrefab at the current position of this GameObject
                GameObject holdingRadius = Instantiate(holdingRadiusPrefab, transform.position, Quaternion.identity);
                // Debug.Log("HoldingRadius spawned.");

                // Destroy the radius after a set time
                Destroy(holdingRadius, duration);

                radiusSpawned = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        radiusSpawned = false;
        // Blocking animation
        animator.SetBool("isBlocking", false);
    }
}