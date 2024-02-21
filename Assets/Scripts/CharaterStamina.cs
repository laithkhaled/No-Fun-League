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

        // If out of breath, stop reducing stamina and start refilling
        if (isOutOfBreath && stamina < totalStamina && !Input.GetKey(KeyCode.LeftShift))
        {
            stamina += staminaIncRate;
        }
        // Otherwise, passively regenerate stamina
        else
        {
            stamina += staminaIncRate;
        }

        // If stamina is refilled, allow the player to run again
        if (stamina >= totalStamina)
        {
            isOutOfBreath = false;
        }

        // Stamina stays in range of 0 - totalStamina
        stamina = Mathf.Clamp(stamina, 0, totalStamina);

        // Reduce stamina bar
        if (staminaBar != null)
        {
            staminaBar.transform.localScale = new Vector2(stamina / totalStamina, staminaBar.transform.localScale.y);
        }
    }
}
