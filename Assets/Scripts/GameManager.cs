using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int downCount = 1;
    private int maxDownCount = 4; // Maximum value for down count

    public TextMeshProUGUI downCountText; // Reference to the TextMeshProUGUI component

    private float firstDownLineX = 0.0f; // X position of the first down line

    // Reference to PlayManager script
    private PlayManager playManager;

    void Start()
    {
        UpdateDownCountText();
        InitializeFirstDownLine();

        // Find and store reference to PlayManager script
        playManager = GameObject.FindObjectOfType<PlayManager>();
    }

    public void InitializeFirstDownLine()
    {
        // Assuming the line of scrimmage starts at the position of the player or a specified object
        GameObject lineOfScrimmage = GameObject.FindGameObjectWithTag("LineOfScrimmage");
        if (lineOfScrimmage != null)
        {
            firstDownLineX = lineOfScrimmage.transform.position.x + 7.8f; // 7.8 units ahead
        }
    }

    public float GetFirstDownLinePosition()
    {
        return firstDownLineX;
    }

    public void PlayerCrossedFirstDown()
    {
        // Logic when the player crosses the first down line
        Debug.Log("Player has crossed the first down line");
        ResetDowns();
    }

    public void IncreaseDownCount()
    {
        if (downCount < maxDownCount)
        {
            downCount++;
            Debug.Log("Down count increased: " + downCount);
            UpdateDownCountText();
            // Delay calling RandomFormation method by 3 seconds
            Invoke("CallRandomFormation", 3f);
        }
        else
        {
            Debug.Log("Down count already at maximum value: " + downCount);
        }
    }

    void CallRandomFormation()
    {
        // Call RandomFormation method from PlayManager script
        if (playManager != null)
        {
            playManager.RandomFormation();
        }
    }

    void UpdateDownCountText()
    {
        if (downCountText != null)
        {
            downCountText.text = "Down: " + downCount.ToString();
        }
    }

    private void ResetDowns()
    {
        downCount = 1; // Resets the down count
        UpdateDownCountText(); // Update the display
    }
}