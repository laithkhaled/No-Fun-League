using System.Collections;
using UnityEngine;

public class DefensiveCoverage : MonoBehaviour
{
    public Transform destination;
    public float speed = 1f; 
    private PlayerController playerController;
    private GameObject ballCarrier = null;
    private Rigidbody2D rb;
    private bool isMoving = false;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Debug.Log(ballCarrier);
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
                    Debug.Log("Ball carrier found: " + player.name);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);

        if (collision.gameObject == ballCarrier)
        {
            Debug.Log("Ball carrier tackled");
            ballCarrier.GetComponent<PlayerController>().GetTackled();
            ballCarrier = null;
        }
    }
}