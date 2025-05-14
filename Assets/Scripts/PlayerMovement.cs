using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement settings
    [Header("Movement settings")]
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float accelerationFroce = 1.5f;
    [SerializeField] private float drag = 0.5f;
    
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping= drag;
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        // get inputs
        float moveHorizontal = Input.GetAxis("Horizontal");

        // stop player if he doesn't press anything
        if (moveHorizontal < 0.01f && moveHorizontal > -0.01f)
        {
            Vector2 force = new Vector2(0, 0);
            rb.AddForce(force);
        }
        // if player's speed is less than the max speed allowed
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            //set the player vector
            Vector2 force = new Vector2(moveHorizontal, 0).normalized * accelerationFroce;
            
            //move player
            rb.AddForce(force);
        }
        
    }
}
