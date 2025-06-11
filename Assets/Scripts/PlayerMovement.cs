using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement settings")] [SerializeField]
    public float maxSpeed = 20f;

    [SerializeField] private float accelerationForce = 9f;
    [SerializeField] private float jumpForce;
    [SerializeField] private float ladderClimbSpeed = 3f;
    [SerializeField] private float ladderSlideSpeed = 1f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private int maxJumps;
    private int jumpsLeft;
    
    public bool isClimbing = false;
    private float originalGravityScale;
    
    [Header("Ice Movement")]
    [SerializeField] private float iceControlReduction = 0.3f;
    
    private float originalFriction;
    private bool isOnIce = false;

    void Start()
    {
        // maxSpeed = 14f;
        // accelerationForce = 5f;
        jumpForce = 15f;

        rb = GetComponent<Rigidbody2D>();
        rb.mass = 2f;
        rb.linearDamping = 3f;
        rb.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.gravityScale = 1.5f;
        originalGravityScale = rb.gravityScale;

        maxJumps = 2;
        jumpsLeft = maxJumps;
        
        originalFriction = rb.linearDamping;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (isClimbing)
        {
            HandleLadderMovement();
        }
        else
        {
            HandleNormalMovement(moveHorizontal);
        }

        if (!isClimbing)
        {
            HandleJumping();
        }
        
        if (isOnIce)
        {
            moveHorizontal *= iceControlReduction;
        }

        if (moveHorizontal < 0.0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveHorizontal > 0.0f)
        {
            spriteRenderer.flipX = false;
        }
    }
    
    public void SetIceFriction(float friction)
    {
        isOnIce = true;
        rb.linearDamping = friction;
    }

    public void ResetIceFriction()
    {
        isOnIce = false;
        rb.linearDamping = originalFriction;
    }

    private void HandleNormalMovement(float moveHorizontal)
    {
        // old code
        // if (rb.linearVelocity.magnitude < maxSpeed && Mathf.Abs(moveHorizontal) > 0.25f)
        // {
        //     Vector2 force = new Vector2(moveHorizontal, 0).normalized * accelerationForce;
        //     rb.AddForce(force);
        // }
        
        if (Mathf.Abs(moveHorizontal) > 0.1f)
        {
            Vector2 force = new Vector2(moveHorizontal, 0) * accelerationForce;
            rb.AddForce(force);
        }

        Vector2 clampedVelocity = rb.linearVelocity;
        clampedVelocity.x = Mathf.Clamp(clampedVelocity.x, -maxSpeed, maxSpeed);
        rb.linearVelocity = clampedVelocity;
    }

    private void HandleLadderMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        
        if (verticalInput > 0.1f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.5f, ladderClimbSpeed);
        }
        else if (verticalInput < -0.1f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.5f, -ladderClimbSpeed);
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.5f, -ladderSlideSpeed);
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            StopClimbing();
            rb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
        }
    }

    private void HandleJumping()
    {
        if (rb.linearVelocity.y == 0.0f)
        {
            jumpsLeft = maxJumps;
        }

        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            float currentJumpForce = jumpsLeft == maxJumps ? jumpForce : jumpForce * 0.9f;
            rb.AddForce(Vector2.up * currentJumpForce, ForceMode2D.Impulse);
            jumpsLeft--;
        }
    }

    public void StartClimbing()
    {
        isClimbing = true;
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.3f, 0);
    }

    public void StopClimbing()
    {
        isClimbing = false;
        rb.gravityScale = originalGravityScale;
    }
}