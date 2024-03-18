using UnityEngine;

public class DefensiveCoverage : MonoBehaviour
{
    public Transform destination;
    public float speed = 5f; 

    private bool isMoving = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isMoving)
        {
            StartMoving();
        }
    }

    void StartMoving()
    {
            isMoving = true;
            StartCoroutine(MoveToDestination()); 
    }

    System.Collections.IEnumerator MoveToDestination()
    {
        while (Vector3.Distance(transform.position, destination.position) > 0.05f)
        {
            // Calculate the direction to move towards the destination
            Vector3 direction = destination.position - transform.position;
            direction.Normalize();
            transform.position += direction * speed * Time.deltaTime;

            yield return null;
        }
        transform.position = destination.position;

        isMoving = false;
    }
}