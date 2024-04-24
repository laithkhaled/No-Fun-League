using UnityEngine;

public class OffensivePlayer : MonoBehaviour
{
    private PlayerController receiverController;
    public GameObject PIRadiusPrefab;
    public float duration = 8f;

    void Start()
    {
        receiverController = GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with a defensive player and this player has the ball
        if (collision.CompareTag("Defense") && receiverController.hasBall)
        {
            receiverController.GetTackled(); // Call the method to handle getting tackled
            
            // Check if the game object has a child named "Football"
            Transform footballTransform = transform.Find("Football(Clone)");
            if (footballTransform != null)
            {
                Destroy(footballTransform.gameObject);
            }
        }
        // Check if the collision is with a defensive player and receiver 
        else if (collision.CompareTag("Defense") && gameObject.CompareTag("Receiver"))
        {
            // Spawn pass interference prefab
            GameObject PIRadius = Instantiate(PIRadiusPrefab, transform.position, Quaternion.identity);

            // Destroy the radius after a set time
            Destroy(PIRadius, duration);
        }
    }
}
