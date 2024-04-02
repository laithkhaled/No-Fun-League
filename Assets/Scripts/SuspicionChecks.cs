using UnityEngine;
using UnityEngine.UI;

public class SuspicionChecks : MonoBehaviour
{
    static float totalSuspicion = 100f;
    static float suspicion;
    public float suspicionIncAmount, suspicionDecAmount;
    private bool suspicionTriggered = false;

    private Slider suspicionSlider;
    private Rigidbody2D rb;
    private GameObject handle;
    private GameObject handle1;
    private GameObject handle2;

    private void Awake()
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

            //Debug.Log("Handle found: " + handle.name);
            //Debug.Log("Handle1 found: " + handle1.name);
            //Debug.Log("Handle2 found: " + handle2.name);
        }
        else
        {
            Debug.LogError("One or more handles not found as great grandchildren of the Slider.");
        }
    }

    private void FixedUpdate()
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
            bool foulDetected = false;
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Foul"))
                {
                    suspicion -= suspicionDecAmount;
                    foulDetected = true;
                    break;
                }
            }

            // If no "Foul" tag was detected, increase suspicion
            if (!foulDetected)
            {
                suspicion += suspicionIncAmount;
            }

            // Clamp suspicion value between 0 and totalSuspicion
            suspicion = Mathf.Clamp(suspicion, 0f, totalSuspicion);

            // Update suspicion slider value
            UpdateSuspicionSliderValue();

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

            // Set the suspicion triggered flag to true
            suspicionTriggered = true;
        }
    }

    void UpdateSuspicionSliderValue()
    {
        // Update the value of the suspicion slider
        if (suspicionSlider != null)
        {
            suspicionSlider.value = suspicion;
        }
    }
}
