using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProximityButton : MonoBehaviour
{
    public Button buttonToEnable;  // Assign this in the inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Ensure your player GameObject has the tag "Player"
        {
            buttonToEnable.interactable = true;  // Enable the button
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            buttonToEnable.interactable = false;  // Disable the button
        }
    }
}
