using UnityEngine;

public class DefensiveChaseController : MonoBehaviour
{
    public float speed = 5f;
    private bool isChasing = false;
    private Transform target; // Reference to the target (receiver with the ball)

    void Update()
    {
        if (isChasing && target != null)
        {
            MoveTowardsTarget();
        }
    }

    public void StartChasing(Transform newTarget)
    {
        isChasing = true;
        target = newTarget;
    }

    public void StopChasing()
    {
        isChasing = false;
        target = null;
    }

    private void MoveTowardsTarget()
    {
        // Calculate direction towards the target
        Vector3 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}