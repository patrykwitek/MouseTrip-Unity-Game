using System;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement settings
    [Header("Movement settings")]
    [SerializeField] public float maxSpeed;
    [SerializeField] private float accelerationForce;
    [SerializeField] private float jumpForce;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private int maxJumps;
    private int jumpsLeft;
    void Start()
    {
        // player stats
        maxSpeed = 3f;
        accelerationForce = 4f;
        jumpForce = 15f;
        // physics setup
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 2f;
        rb.linearDamping = 3f;
        rb.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.gravityScale = 1.5f;
        //movement
        maxJumps = 2;
        jumpsLeft = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        // get inputs
        float moveHorizontal = Input.GetAxis("Horizontal");

        // if player's speed is less than the max speed allowed
        if (rb.linearVelocity.magnitude < maxSpeed && (moveHorizontal > 0.25 || moveHorizontal < -0.25))
        {
            //set the player vector
            Vector2 force = new Vector2(moveHorizontal, 0).normalized * accelerationForce;
            
            //move player
            rb.AddForce(force);
        }

        if (rb.linearVelocity[1] == 0.0f)
        {
            jumpsLeft = maxJumps;
        }
        // jumping
        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            if (jumpsLeft == maxJumps)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.up * (jumpForce * 0.9f), ForceMode2D.Impulse);
            }
            jumpsLeft--;
        }
        
        
        // Turn the character based on his direction of movement
        if (moveHorizontal < 0.0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveHorizontal > 0.0f)
        {
            spriteRenderer.flipX = false;
        }
    }

}
