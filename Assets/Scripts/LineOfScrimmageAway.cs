using UnityEngine;
using System.Collections;

public class LineOfScrimmageAway : MonoBehaviour
{
   private Vector3 targetPosition;
   public float teleportDelay = 2f; // Delay before teleporting
   public bool isTeleporting = false;

   void Start()
   {
       // Subscribe to the event when the player is tackled
       PlayerController.OnPlayerTackled += MoveToTackledPosition;
   }

   void OnDestroy()
   {
       // Unsubscribe from the event when the Line Of Scrimmage object is destroyed
       PlayerController.OnPlayerTackled -= MoveToTackledPosition;
   }

   void MoveToTackledPosition(Vector3 tackledPosition)
   {
       // Set the target position for the Line Of Scrimmage object
       targetPosition = new Vector3(tackledPosition.x, transform.position.y, transform.position.z);
       // Start the coroutine to teleport after a delay
       StartCoroutine(TeleportAfterDelay());
   }

   IEnumerator TeleportAfterDelay()
   {
       // Set teleporting flag to true
       isTeleporting = true;
       // Wait for the delay
       yield return new WaitForSeconds(teleportDelay);
       // Teleport to the target position
       transform.position = targetPosition;
       // Set teleporting flag to false
       isTeleporting = false;
   }
}