using UnityEngine;

public class OffensivePlayer : MonoBehaviour
{
   private PlayerController receiverController;

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
           Debug.Log("Triggered by OffensiveLineMan and Defense.");
       }
   }
}