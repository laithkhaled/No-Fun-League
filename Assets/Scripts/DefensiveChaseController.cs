using UnityEngine;

public class DefensiveChaseController : MonoBehaviour
{
    public float speed = 5f;
    private GameObject ballCarrier = null;
    private Rigidbody2D rb;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        FindBallCarrier();
        ChaseBallCarrier();
    }

    private void FindBallCarrier()
    {
        // Only search for the ball carrier if there isn't already one identified
        if (ballCarrier == null || !ballCarrier.GetComponent<PlayerController>().hasBall)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Receiver");
            foreach (var player in players)
            {
                if (player.GetComponent<PlayerController>().hasBall)
                {
                    ballCarrier = player;
                    break; // Stop searching once the ball carrier is found
                }
            }
        }
    }

    private void ChaseBallCarrier()
    {
        if (ballCarrier != null)
        {
            float chaseSpeed = speed * 1.1f; // Chase at 110% of the ball carrier's speed

            // Calculate direction towards the ball carrier
            Vector2 direction = (ballCarrier.transform.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * chaseSpeed * Time.deltaTime);

            float movementSpeed = direction.magnitude;
            animator.SetFloat("Speed", movementSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == ballCarrier)
        {
            // Stop the ball carrier
            ballCarrier.GetComponent<PlayerController>().StopMovement();
            // Optionally, reset ball carrier reference if needed
            ballCarrier = null;
        }
    }
}