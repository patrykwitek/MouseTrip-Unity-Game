using System.Collections;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool flipSprite = true; // Toggle sprite flipping
    
    private Transform currentTarget;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Set initial target
        currentTarget = pointB;
        
        // Get sprite renderer if exists
        if (flipSprite)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogWarning("No SpriteRenderer found - flipping disabled");
                flipSprite = false;
            }
        }
    }

    private void Update()
    {
        // Move towards current target
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        
        // Check if reached target
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.01f)
        {
            // Switch targets
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
            
            // Flip sprite if enabled
            if (flipSprite)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
                // Alternative flip method:
                // transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    // Visualize the path in editor
    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawSphere(pointA.position, 0.1f);
            Gizmos.DrawSphere(pointB.position, 0.1f);
        }
    }
}
