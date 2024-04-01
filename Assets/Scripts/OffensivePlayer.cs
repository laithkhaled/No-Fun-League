using UnityEngine;

public class OffensivePlayer : MonoBehaviour
{
    private PlayerController receiverController;

    void Start()
    {
        receiverController = GetComponent<PlayerController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a defensive player and this player has the ball
        if (collision.gameObject.CompareTag("Defense") && receiverController.hasBall)
        {
            receiverController.GetTackled(); // Call the method to handle getting tackled
        }
    }
}