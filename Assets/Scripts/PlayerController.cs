using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public bool hasBall = false;
    private Transform endZoneTarget = null;
    private bool isMoving = false; 
    public bool isTackled = false;


    void Start()
    {
        FindEndZoneTarget();
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
        transform.position += direction * speed * Time.deltaTime;
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
        Debug.Log("Player is tackled");
        Debug.Log(isTackled);
    }

    public void CatchBall()
    {
        hasBall = true;
        isMoving = true; 
    }
}