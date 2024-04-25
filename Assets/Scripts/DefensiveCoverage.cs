using System;
using UnityEngine;

public class DefensiveCoverage : MonoBehaviour
{
    public Transform destination;
    public float speed = 1f; 
    private PlayerController playerController;
    private GameObject ballCarrier = null;
    private Rigidbody2D rb;
    private bool isMoving = false;

    // Static event to be shared across all instances of DefensiveCoverage
    public static event Action BallCarrierTackledEvent;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        
        // Subscribe to the static event
        BallCarrierTackledEvent += RespondToTackleEvent;
    }

    private void Update()
    {
        FindBallCarrier(); // Always check for a ball carrier

        if (Input.GetKeyDown(KeyCode.F) && !isMoving)
        {
            StartMovingToDestination();
        }

        // Prioritize chasing the ball carrier if one is identified
        if (ballCarrier != null)
        {
            ChaseBallCarrier();
        }
        else if (isMoving)
        {
            // Move to the destination if not chasing the ball carrier
            MoveToDestination();
        }
    }

    public void StartMovingToDestination()
    {
        isMoving = true;
    }

    private void MoveToDestination()
    {
        if (destination != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.position, playerController.speed * Time.deltaTime);
            if (transform.position == destination.position)
            {
                StopMovement();
            }
        }
    }

    public void StopMovement()
    {
        isMoving = false;
    }

    private void FindBallCarrier()
    {
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
            Vector3 direction = (ballCarrier.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.position = Vector3.MoveTowards(transform.position, ballCarrier.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject == ballCarrier)
        {
            // Invoke the static event to notify all instances that the ball carrier has been tackled
            BallCarrierTackledEvent?.Invoke();
        }
    }

    // Method to respond to the tackle event
    public void RespondToTackleEvent()
    {
        ballCarrier = null;
    }
}
