using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static bool isRunning, isThrowing;
    public float moveSpeed;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveForce;
        Vector2 PlayerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Player walking and running speed
        if (isRunning == true)
        {
             moveForce = PlayerInput * moveSpeed * 2f;
        }
        else
        { 
            moveForce = PlayerInput * moveSpeed;
        }

        rb.velocity = moveForce;
    }
}
