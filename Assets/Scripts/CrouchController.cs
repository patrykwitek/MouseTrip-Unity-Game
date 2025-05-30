using UnityEngine;

public class CrouchController : MonoBehaviour
{
    [Header("Collider Settings")]
    [SerializeField] private Collider2D standingCollider;
    [SerializeField] private Collider2D crouchingCollider;
    
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string crouchAnimParam = "isCrouching";
    
    [Header("Movement")]
    [SerializeField] private float crouchSpeedMultiplier = 0.5f;
    [SerializeField] private LayerMask obstacleLayers;
    
    private float originalSpeed;
    private PlayerMovement movementController;

    private void Awake()
    {
        movementController = GetComponent<PlayerMovement>();
        originalSpeed = movementController.maxSpeed;
        
        if (crouchingCollider != null) crouchingCollider.enabled = false;
        if (standingCollider != null) standingCollider.enabled = true;
    }

    private void Update()
    {
        bool ctrlPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        if (ctrlPressed && !crouchingCollider.enabled)
        {
            StartCrouching();
        }
        else if (!ctrlPressed && crouchingCollider.enabled)
        {
            StopCrouching();
        }
    }

    private void StartCrouching()
    {
        if (animator != null)
        {
            animator.SetBool(crouchAnimParam, true);
            animator.Play("Crouch", 0, 0f);
        }
        
        standingCollider.enabled = false;
        crouchingCollider.enabled = true;
        
        //if (movementController != null)
            //movementController.maxSpeed = originalSpeed * crouchSpeedMultiplier;
    }

    private void StopCrouching()
    {
        if (!CheckHeadClearance())
        {
            Debug.Log("Can't stand up - obstacle above");
            return;
        }

        crouchingCollider.enabled = false;
        standingCollider.enabled = true;
        
        if (animator != null)
            animator.SetBool(crouchAnimParam, false);
        
        //if (movementController != null)
            //movementController.maxSpeed = originalSpeed;
    }

    private bool CheckHeadClearance()
    {
        if (standingCollider == null || crouchingCollider == null)
            return true;

        float heightDifference = standingCollider.bounds.size.y - crouchingCollider.bounds.size.y;
        
        Vector2[] raycastOrigins = new Vector2[]
        {
            new Vector2(standingCollider.bounds.center.x, crouchingCollider.bounds.max.y),
            new Vector2(standingCollider.bounds.min.x + 0.1f, crouchingCollider.bounds.max.y),
            new Vector2(standingCollider.bounds.max.x - 0.1f, crouchingCollider.bounds.max.y)
        };

        foreach (Vector2 origin in raycastOrigins)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, heightDifference + 0.05f, obstacleLayers);
            Debug.DrawRay(origin, Vector2.up * (heightDifference + 0.05f), Color.cyan, 0.5f);
            
            if (hit.collider != null && !hit.collider.isTrigger)
            {
                return false;
            }
        }
        
        return true;
    }
}