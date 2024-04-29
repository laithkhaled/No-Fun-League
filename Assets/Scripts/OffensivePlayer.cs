using UnityEngine;

public class OffensivePlayer : MonoBehaviour
{
    private PlayerController receiverController;
    public GameObject PIRadiusPrefab;
    float duration = 6f;
    private bool radiusSpawned = false;

    void Start()
    {
        receiverController = GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Generate a random integer between 1 and 2
        int randomChoice = Random.Range(1, 6);

        // Check if the collision is with a defensive player and this player has the ball
        if (collision.CompareTag("Defense") && receiverController.hasBall)
        {
            // Check if the game object has a child named "Football"
            Transform footballTransform = transform.Find("Football(Clone)");
            if (footballTransform != null)
            {
                Destroy(footballTransform.gameObject);
            }
            
            receiverController.GetTackled(); // Call the method to handle getting tackled
        }
        // Check if the collision is with a defensive player and receiver 
        else if (collision.CompareTag("Defense") && gameObject.CompareTag("Receiver") && !radiusSpawned && randomChoice == 1)
        {
            // Spawn pass interference prefab
            GameObject PIRadius = Instantiate(PIRadiusPrefab, transform.position, Quaternion.identity);
            // Debug.Log("PIRadius spawned.");

            // Destroy the radius after a set time
            Destroy(PIRadius, duration);

            radiusSpawned = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        radiusSpawned = false;
    }
}
