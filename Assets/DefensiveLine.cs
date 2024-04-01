using System.Collections;
using UnityEngine;

public class DefensiveLine : MonoBehaviour
{
    private PlayerController receiverController;

    void Start()
    {
        receiverController = GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the trigger collision is with an Offensive Lineman
        if (other.CompareTag("OffensiveLineman"))
        {
            receiverController.StopMovement(); // Call the method to handle getting tackled
             Debug.Log("Triggered by Offensive Lineman");
        }
    }
}