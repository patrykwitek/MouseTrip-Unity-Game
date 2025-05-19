using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement settings
    [Header("Movement settings")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerationFroce;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    void Start()
    {
        maxSpeed = 60f;
        accelerationFroce = 7f;
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 2f;
        rb.linearDamping= 3f;
        rb.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // get inputs
        float moveHorizontal = Input.GetAxis("Horizontal");


        // if player's speed is less than the max speed allowed
        if (rb.linearVelocity.magnitude < maxSpeed && moveHorizontal > 0.25 || moveHorizontal < -0.25)
        {
            //set the player vector
            Vector2 force = new Vector2(moveHorizontal, 0).normalized * accelerationFroce;
            
            //move player
            rb.AddForce(force);
        }
        // Turn the character based on his direction of movement
        if (rb.linearVelocity[0] < 0.00f)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
