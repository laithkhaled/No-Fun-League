using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int downCount = 3;
    private int maxDownCount = 4; // Maximum value for down count

    public TextMeshProUGUI downCountText;

    private float firstDownLineX = 0.0f; // X position of the first down line

    private PlayManager playManager;

    public GameObject lineOfScrimmage;
    public GameObject lineOfScrimmageAway;

    public PlayManagerAway playManagerAway;

    public TeamSwapper teamSwapper;

    void Start()
    {
        UpdateDownCountText();
        InitializeFirstDownLine();

        // Find and store reference to PlayManager script
        playManager = GameObject.FindObjectOfType<PlayManager>();
    }

    public void InitializeFirstDownLine()
    {
        GameObject firstDownLine = GameObject.FindGameObjectWithTag("FirstDownLine");
        if (firstDownLine != null)
        {
            GameObject lineOfScrimmage = GameObject.FindGameObjectWithTag("LineOfScrimmage");
            if (lineOfScrimmage != null)
            {
                Debug.Log("Line of Scrimmage position: " + lineOfScrimmage.transform.position);
                firstDownLineX = lineOfScrimmage.transform.position.x + 7.8f; // 7.8 units ahead
                Vector3 newPosition = new Vector3(firstDownLineX, firstDownLine.transform.position.y, firstDownLine.transform.position.z);
                firstDownLine.transform.position = newPosition;
                Debug.Log("First Down Line X position: " + firstDownLineX);
            }
            else
            {
                Debug.LogError("Line of Scrimmage not found or tagged incorrectly!");
            }
        }
        else
        {
            Debug.LogError("First down line object not found or tagged incorrectly!");
        }
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
    }
    else
    {
        Debug.Log("Maximum down count reached, swapping positions.");
        teamSwapper.SwapTeams();
        ResetDowns();
    }
}

    public void CallRandomFormationBoth()
    {
        if (playManager != null && playManagerAway != null)
        {
            playManager.RandomFormation();
            playManagerAway.RandomFormation();
            IncreaseDownCount();
            //Debug.Log("Called RandomFormation on both PlayManager and PlayManagerAway.");
        }
        else
        {
            Debug.LogError("One of the PlayManagers is not assigned.");
            }
    }

    void UpdateDownCountText()
    {
        if (downCountText != null)
        {
            downCountText.text = "Down: " + downCount.ToString();
        }
    }

    public void ResetDowns()
    {
       /* if (downCount == 1)
        {
            // Teleport the first down line object 7.8 units ahead
            GameObject firstDownLine = GameObject.FindGameObjectWithTag("FirstDownLine");
            if (firstDownLine != null)
            {
                Vector3 newPosition = new Vector3(firstDownLineX, firstDownLine.transform.position.y, firstDownLine.transform.position.z);
                firstDownLine.transform.position = newPosition;
                Debug.Log("First down line teleported to: " + newPosition);
            }
            else
            {
                Debug.LogError("First down line object not found or tagged incorrectly!");
            }
        }*/ 
    
        downCount = 1; // Resets the down count
        UpdateDownCountText(); // Update the display
    }

    public void TeleportLineOfS_Away(Vector3 position)
    {
        lineOfScrimmageAway.transform.position = position;
    }

    public void TeleportLineOfS(Vector3 position)
    {   
        lineOfScrimmage.transform.position = position;
    }

}