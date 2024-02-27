using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterStamina : MonoBehaviour
{
    float totalStamina = 100, stamina;
    public float staminaDcrRate, staminaIncRate;
    public GameObject staminaBar;
    bool isOutOfBreath;

    // Start is called before the first frame update
    void Awake()
    {
        stamina = totalStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOutOfBreath != true)
        {
            // Reduces stamina bar if player if running
            if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && !isOutOfBreath)
            {
                CharacterMovement.isRunning = true;
                stamina -= staminaDcrRate;

                // If stamina reaches 0 then player is out of breath and cannot sprint 
                // until stamina refills
                if (stamina <= 0)
                {
                    isOutOfBreath = true;
                    CharacterMovement.isRunning = false;
                }
            }
            else
            {
                CharacterMovement.isRunning = false;
            }
        }

        // If stamina is refilled, allow the player to run again
        if (stamina >= totalStamina)
        {
            isOutOfBreath = false;
        }

        // Delay initially when out of breath
        if (isOutOfBreath == true && stamina == 0)
        {
            Invoke("CatchBreath", 1.5f);
            // Debug.Log("Caught Breath.");
        }
        else if (isOutOfBreath && Input.GetKey(KeyCode.LeftShift))
        {
            // nothing happens
        }
        // Otherwise, passively regenerate stamina
        else
        {
            stamina += staminaIncRate * 0.40f;
        }

        // Stamina stays in range of 0 - totalStamina
        stamina = Mathf.Clamp(stamina, 0, totalStamina);

        // Reduce stamina bar
        if (staminaBar != null)
        {
            staminaBar.transform.localScale = new Vector2(stamina / totalStamina, staminaBar.transform.localScale.y);
        }
    } 

   void CatchBreath()
    {
        // If out of breath, stop reducing stamina and start refilling
        if (isOutOfBreath && !Input.GetKey(KeyCode.LeftShift))
        {
            stamina += staminaIncRate;
        }
    }
}
