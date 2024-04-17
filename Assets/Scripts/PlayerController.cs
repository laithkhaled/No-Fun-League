using UnityEngine;
using System;
using System.Collections;

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

    private GameManager gameManager;

    void Start()
    {
        FindEndZoneTarget();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (hasBall && isMoving && endZoneTarget != null)
        {
            MoveTowardsEndZone();
            CheckFirstDownCross();
        }
    }

    private void FindEndZoneTarget()
    {
        GameObject endZone = GameObject.FindGameObjectWithTag("Endzone1");
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
        animator.SetBool("getsBall", false);
        animator.SetBool("isRunningBall", true);
    }

    private void CheckFirstDownCross()
    {
        if (gameManager != null && transform.position.x >= gameManager.GetFirstDownLinePosition())
        {
            gameManager.PlayerCrossedFirstDown();
        }
    }

    public void StopMovement()
    {
        isMoving = false;
    }

    public void GetTackled()
    {
        isMoving = false;
        hasBall = false;
        isTackled = true;
        StopMovement();
        animator.SetBool("isTackled", true);
        Debug.Log("Player is tackled");
        Debug.Log(isTackled);

        OnPlayerTackled(transform.position);

        // Inform the game manager that the player was tackled
        if (gameManager != null)
        {
            gameManager.IncreaseDownCount();
        }
    }

    public void CatchBall()
    {
        hasBall = true;
        isMoving = true;
        animator.SetBool("getsBall", true);
    }
}