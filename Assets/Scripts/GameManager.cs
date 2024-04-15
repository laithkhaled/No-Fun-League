using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int downCount = 1;
    private int maxDownCount = 4; // Maximum value for down count

    public TextMeshProUGUI downCountText; // Reference to the TextMeshProUGUI component

    void Start()
    {
        UpdateDownCountText();
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
            Debug.Log("Down count already at maximum value: " + downCount);
        }
    }

    void UpdateDownCountText()
    {
        if (downCountText != null)
        {
            downCountText.text = "Down: " + downCount.ToString();
        }
    }
}

