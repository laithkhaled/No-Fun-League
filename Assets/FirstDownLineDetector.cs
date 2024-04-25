using UnityEngine;

public class FirstDownLineDetector : MonoBehaviour
{
    // Reference to the GameManager script
    public GameManager gameManager;

    // Flag to prevent multiple triggers
    private bool hasCrossed = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided with the first down line has the "Football" tag
        if (other.transform.parent.CompareTag("Receiver"))
        {
            Debug.Log("Receiver collided with first down line");
            // Check if the football's parent is the player
            if (other.transform.parent != null && other.gameObject.name == "Football(Clone)" && !hasCrossed)
            {
                // Player has crossed the first down line with the football
                Debug.Log("Player has crossed the first down line");
                gameManager.ResetDowns(); // Call the GameManager method
                hasCrossed = true; // Set the flag to prevent multiple triggers
            }
        }
    }
}
