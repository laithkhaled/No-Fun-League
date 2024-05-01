using UnityEngine;
using UnityEngine.UI;

public class SuspicionChecks : MonoBehaviour
{
    static float totalSuspicion = 100f;
    static float suspicion;
    static public float suspicionIncAmount = 25, suspicionDecAmount = 10;
    private bool suspicionTriggered = false;

    static Slider suspicionSlider;
    private Rigidbody2D rb;
    static GameObject handle;
    static GameObject handle1;
    static GameObject handle2;

    static int correctChoice = 0;
    private int playerChoice = 0;
    private int randomFalseFoul = 0;
    static bool foulDetected = false;

    private void Start()
    {
        suspicionSlider = GameObject.FindGameObjectWithTag("SuspicionMeter").GetComponent<Slider>();
        rb = GetComponent<Rigidbody2D>();

        // Find the handles dynamically
        Transform handleTransform = suspicionSlider.transform.Find("Fill Area/Fill/Handle");
        Transform handle1Transform = suspicionSlider.transform.Find("Fill Area/Fill/Handle1");
        Transform handle2Transform = suspicionSlider.transform.Find("Fill Area/Fill/Handle2");

        if (handleTransform != null && handle1Transform != null && handle2Transform != null)
        {
            handle = handleTransform.gameObject;
            handle1 = handle1Transform.gameObject;
            handle2 = handle2Transform.gameObject;
        }
        else
        {
            Debug.LogError("One or more handles not found as great grandchildren of the Slider.");
        }

        suspicion = suspicionSlider.value;

        randomFalseFoul = Random.Range(1, 4);
    }

    private void Update()
    {
        // If suspicion reaches 100 then game over
        if (suspicion == 100f)
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.GameOver();
            }

            // Reset suspicion for reload
            suspicion = 0;
        }

        // Check if the object is not moving
        if (rb.velocity.magnitude <= 0.01f && !suspicionTriggered)
        {
            // Get all colliders overlapping with flag collider
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<Collider2D>().bounds.size, 0f);

            // Check each collider for the "Foul" tag
            foreach (Collider2D collider in colliders)
            {
                // True foul checks
                if (collider.CompareTag("Holding"))
                {
                    suspicion -= suspicionDecAmount;
                    foulDetected = true;
                    correctChoice = 1;
                    break;
                }
                else if (collider.CompareTag("PassInterference")) {
                    suspicion -= suspicionDecAmount;
                    foulDetected = true;
                    correctChoice = 2;
                    break;
                }
                else if (collider.CompareTag("RoughHousing")) {
                    suspicion -= suspicionDecAmount;
                    foulDetected = true;
                    correctChoice = 3;
                    break;
                }
            }

            // False calls
            if (!foulDetected)
            {
                suspicion += suspicionIncAmount;
                //Debug.Log(suspicion);
            }

            // Clamp suspicion value between 0 and totalSuspicion
            suspicion = Mathf.Clamp(suspicion, 0f, totalSuspicion);

            // Update suspicion slider value
            UpdateSuspicionSliderValue();

            // Set the suspicion triggered flag to true
            suspicionTriggered = true;
        }
    }

    void UpdateSuspicionSliderValue()
    {

        suspicionSlider.value = suspicion;

        // Activate/deactivate handles based on suspicion level
        if (suspicion < 33)
        {
            handle.SetActive(true);
            handle1.SetActive(false);
            handle2.SetActive(false);
        }
        else if (suspicion >= 33 && suspicion < 66)
        {
            handle.SetActive(false);
            handle1.SetActive(true);
            handle2.SetActive(false);
        }
        else
        {
            handle.SetActive(false);
            handle1.SetActive(false);
            handle2.SetActive(true);
        }

        // Debug.Log("Suspicion: " + suspicion);
    }

    // Call the appropriate choice method based on the correct choice
    void HandleChoice()
    {
        // True foul
        if (foulDetected)
        {
            if (playerChoice == correctChoice)
            {
                suspicion -= suspicionIncAmount / 2;
            }
            else
            {
                suspicion += suspicionIncAmount / 2;
            }
        }
        // False foul
        else
        {
            if (playerChoice == randomFalseFoul)
            {
                suspicion -= suspicionIncAmount / 2;
            }
            else
            {
                suspicion += suspicionIncAmount / 2;
            }
        }

        // Debug.Log("Suspicion in handle choice: " + suspicion);

        // Clamp suspicion value between 0 and totalSuspicion
        suspicion = Mathf.Clamp(suspicion, 0f, totalSuspicion);

        UpdateSuspicionSliderValue();
    }

    // Flag call button choices
    public void ChooseHolding()
    {
        playerChoice = 1;
        HandleChoice();
    }
    public void ChoosePassInterference()
    {
        playerChoice = 2;
        HandleChoice();
    }
    public void ChooseRoughHousing()
    {
        playerChoice = 3;
        HandleChoice();
    }

    // Static method to modify suspicion increase amount
    public static void ModifySuspicionIncAmount(float amount)
    {
        suspicionIncAmount = amount;
        Debug.Log(suspicionIncAmount);
    }

    // Static method to modify suspicion decrease amount
    public static void ModifySuspicionDecAmount(float amount)
    {
        suspicionDecAmount = amount;
        Debug.Log(suspicionDecAmount);
    }
}
