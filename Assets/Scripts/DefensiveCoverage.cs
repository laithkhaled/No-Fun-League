using System.Collections;
using UnityEngine;

public class DefensiveCoverage : MonoBehaviour
{
    public Transform destination;
    private PlayerController playerController;

    private bool isMoving = false;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isMoving)
        {
            StartMoving();
        }
    }

    public void StartMoving()
    {
        isMoving = true;
        StartCoroutine(MoveToDestination());
    }

    private IEnumerator MoveToDestination()
    {
        while (Vector3.Distance(transform.position, destination.position) > 0.05f)
        {
            Vector3 direction = destination.position - transform.position;
            direction.Normalize();
            transform.position += direction * playerController.speed * Time.deltaTime;

            yield return null;
        }
        transform.position = destination.position;
        isMoving = false;
    }

    public void StopMovement()
    {
        StopAllCoroutines();
        isMoving = false; 
        Debug.Log("Defensive player movement stopped.");
    }
}