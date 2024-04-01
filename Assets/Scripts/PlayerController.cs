using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public bool hasBall = false;
    private Transform endZoneTarget = null;
    private bool isBlocked = false;

    void Start()
    {
        FindEndZoneTarget();
    }

    void Update()
    {
        if (hasBall && !isBlocked && endZoneTarget != null)
        {
            MoveTowardsEndZone();
        }
    }

    private void FindEndZoneTarget()
    {
        GameObject endZone = GameObject.FindGameObjectWithTag("Endzone1");
        endZoneTarget = endZone.transform;
    }

    private void MoveTowardsEndZone()
    {
        // Calculate direction towards the end zone
        Vector2 direction = (endZoneTarget.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void StopMovement()
    {
        isBlocked = true;
        speed = 0; // Stop the player's movement
        Debug.Log("Defensive Player is stopped");
    }

    public void GetTackled()
    {
        hasBall = false;
        speed = 0; 
        SetLineOfScrimmage(); 
    }

    private void SetLineOfScrimmage()
    {
        GameObject lineOfScrimmage = GameObject.FindWithTag("LineOfScrimmage");
        if (lineOfScrimmage != null)
        {
            lineOfScrimmage.transform.position = transform.position;
        }
    }

    public void CatchBall()
    {
        hasBall = true;
    }

    public void LoseBall()
    {
        hasBall = false;
    }
    
    public void Blocked()
    {
        StopMovement();
    }
    
}