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

    void Start()
    {
        FindEndZoneTarget();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (hasBall && isMoving && endZoneTarget != null)
        {
            MoveTowardsEndZone();
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
        // Find the LevelManager script 
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.EndPlay();
        }
        else
        {
            Debug.LogError("LevelManager not found in the scene!");
        }
        Debug.Log("Player is tackled");
        Debug.Log(isTackled);
        OnPlayerTackled(transform.position);
    }

    public void CatchBall()
    {
        hasBall = true;
        animator.SetBool("getsBall", true);
        isMoving = true; 
    }
}