using UnityEngine;
using UnityEngine.UI;

public class SuspicionChecks : MonoBehaviour
{
    static float totalSuspicion = 100f;
    static float suspicion;
    public float suspicionIncAmount;
    private bool suspicionTriggered = false;

    private Slider suspicionSlider;
    private Rigidbody2D rb;

    private void Awake()
    {
        suspicionSlider = GameObject.FindGameObjectWithTag("SuspicionMeter").GetComponent<Slider>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void FixedUpdate()
    {
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
                    foulDetected = true;
                    break;
                }
            }

            // If no "Foul" tag was detected, increase suspicion
            if (!foulDetected)
            {
                suspicion += suspicionIncAmount;

                // Clamp suspicion value between 0 and totalSuspicion
                suspicion = Mathf.Clamp(suspicion, 0f, totalSuspicion);

                // Update suspicion slider value
                UpdateSuspicionSliderValue();

                // Set the suspicion triggered flag to true
                suspicionTriggered = true;
            }
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
