using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public bool hasBall = false;
    private Transform endZoneTarget = null;
    private bool isMoving = false; 
    public bool isTackled = false;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public static event Action<Vector3> OnPlayerTackled;

    private Vector3 previousPosition;

    public GameManager gameManager;

    public GameObject endZone;

    void Start()
    {
        FindEndZoneTarget();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        previousPosition = transform.position;
    }

    void Update()
    {
        if (hasBall && isMoving && endZoneTarget != null)
        {
            MoveTowardsEndZone();
            animator.SetBool("getsBall", false);
            animator.SetBool("isRunningBall", true);
        }

        UpdateAnimatorSpeed();
    }

    private void UpdateAnimatorSpeed()
    {
        // Calculate velocity based on change in position
        Vector3 velocity = (transform.position - previousPosition) / Time.deltaTime;
        float playerSpeed = velocity.magnitude;

        // Update animator parameter
        animator.SetFloat("Speed", playerSpeed);

        // Update previous position
        previousPosition = transform.position;
    }


    private void FindEndZoneTarget()
    {
        if (endZone != null)
        {
            endZoneTarget = endZone.transform;
        }
    }

    private void MoveTowardsEndZone()
    {
        Vector3 direction = (endZoneTarget.position - transform.position).normalized;

        float runSpeed = speed / 2;
        transform.position += direction * runSpeed * Time.deltaTime;

        // Flip the sprite based on movement direction
        if (direction.x > 0) // Moving right
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0) // Moving left
        {
            spriteRenderer.flipX = true;
        }
    }

    public void StopMovement()
    {
        isMoving = false; 
    }

    public void GetTackled()
    {
        hasBall = false;
        isTackled = true;
        StopMovement();
        animator.SetBool("getsBall", false);
        animator.SetBool("isRunningBall", false);
        animator.SetBool("isTackled", true);
        // Find the LevelManager script 
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.EndPlay();
        GameManager gameManager = FindObjectOfType<GameManager>();
        Invoke("CallRandomFormationWithDelay", 4.5f);
        OnPlayerTackled(transform.position);
    }

    void CallRandomFormationWithDelay()
    {
        isTackled = false;
        animator.SetBool("isTackled", false);
        gameManager.CallRandomFormationBoth();
        gameManager.IncreaseDownCount();
    }

    public void CatchBall()
    {
        hasBall = true;
        animator.SetBool("getsBall", true);
        isMoving = true; 
    }
}