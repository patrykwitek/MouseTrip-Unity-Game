using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement settings
    [Header("Movement settings")]
    [SerializeField] private float maxSpeed = 12f;
    [SerializeField] private float accelerationFroce = 5f;
    
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 2;
        rb.linearDamping= 4f;
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        // get inputs
        float moveHorizontal = Input.GetAxis("Horizontal");


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
