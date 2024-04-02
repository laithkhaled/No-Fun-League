using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public bool hasBall = false;
    private Transform endZoneTarget = null;
    private bool isBlocked = false;
    Rigidbody2D rb;

    void Start()
    {
        FindEndZoneTarget();
        rb = GetComponent<Rigidbody2D>();
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

    public void StopMovement(Rigidbody2D rb)
    {
        rb.velocity = Vector2.zero; 
        rb.isKinematic = true;
        if(rb.isKinematic == true && rb.velocity == Vector2.zero){
            Debug.Log("Kinematic is true");
        }
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
 
    }

    public void CatchBall()
    {
        hasBall = true;
    }
    
}