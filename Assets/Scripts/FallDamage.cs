using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private float minFallHeight = 2f;
    [SerializeField] private float damageFallHeight = 4f;
    [SerializeField] private float lethalFallHeight = 8f;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Effects")]
    [SerializeField] private ParticleSystem landEffect;
    [SerializeField] private AudioClip landSound;

    private float fallStartY;
    private bool isFalling;
    private Rigidbody2D rb;
    private bool wasGroundedLastFrame;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (healthSystem == null)
            healthSystem = GetComponent<HealthSystem>();
    }

    private void Update()
    {
        bool isGrounded = IsGrounded();

        if (!isGrounded && rb.linearVelocity.y < -0.5f && !isFalling && !wasGroundedLastFrame)
        {
            StartFalling();
        }
        else if (isGrounded && isFalling)
        {
            StopFalling();
        }

        wasGroundedLastFrame = isGrounded;
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position,
            new Vector2(0.6f, 0.6f),
            0f,
            Vector2.down,
            groundCheckDistance,
            groundLayer);

        Debug.DrawRay(transform.position, Vector2.down * (groundCheckDistance + 0.05f), Color.red);
        return hit.collider != null;
    }

    private void StartFalling()
    {
        if (transform.position.y > GetGroundLevel())
        {
            isFalling = true;
            fallStartY = transform.position.y;
            Debug.Log($"Started falling from {fallStartY}");
        }
    }

    private float GetGroundLevel()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            10f,
            groundLayer);
        
        return hit.point.y;
    }

    private void StopFalling()
    {
        isFalling = false;
        float fallHeight = Mathf.Abs(fallStartY - transform.position.y);
        Debug.Log($"Landed after falling {fallHeight} units");

        if (fallHeight >= lethalFallHeight)
        {
            Debug.Log("Lethal fall damage");
            healthSystem.TakeDamage(3);
            PlayLandEffects();
        }
        else if (fallHeight >= damageFallHeight)
        {
            Debug.Log("Normal fall damage");
            healthSystem.TakeDamage(1);
            PlayLandEffects();
        }
        else if (fallHeight >= minFallHeight)
        {
            Debug.Log("Soft landing");
            PlayLandEffects();
        }
    }

    private void PlayLandEffects()
    {
        if (landEffect != null)
            Instantiate(landEffect, transform.position, Quaternion.identity);

        if (landSound != null)
            AudioSource.PlayClipAtPoint(landSound, transform.position);
    }
}