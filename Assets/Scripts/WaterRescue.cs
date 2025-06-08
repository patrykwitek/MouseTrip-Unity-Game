using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterRescue : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    
    [Header("Ustawienia")]
    public Tilemap groundTilemap;
    public float checkDistance = 3f;
    public float edgeOffset = 0.2f;
    public Vector2 raycastOffset = new Vector2(0, -0.3f);
    
    private void Awake()
    {
        if (healthSystem is null)
            healthSystem = GetComponent<HealthSystem>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 rescuePos = FindRescuePosition(other.transform.position);
            if (rescuePos != Vector2.zero)
            {
                other.transform.position = rescuePos;
                ResetPlayerVelocity(other);
                healthSystem.TakeDamage(1);
            }
        }
    }

    private Vector2 FindRescuePosition(Vector2 playerPos)
    {
        Vector2 checkOrigin = (Vector2)playerPos + raycastOffset;
        
        Vector2 leftPos = CheckDirection(checkOrigin, Vector2.left, checkDistance);
        if(leftPos != Vector2.zero) return leftPos;

        return CheckDirection(checkOrigin, Vector2.right, checkDistance * 1.5f);
    }

    private Vector2 CheckDirection(Vector2 origin, Vector2 direction, float distance)
    {
        for (float yOffset = -0.5f; yOffset <= 0.5f; yOffset += 0.25f)
        {
            Vector2 startPos = origin + new Vector2(0, yOffset);
            RaycastHit2D hit = Physics2D.Raycast(
                startPos, 
                direction, 
                distance);

            Debug.DrawRay(startPos, direction * distance, Color.red, 2f);

            if (hit.collider != null && hit.collider.GetComponent<TilemapCollider2D>())
            {
                return new Vector2(
                    hit.point.x + (direction.x > 0 ? edgeOffset : -edgeOffset),
                    hit.point.y + 0.5f);
            }
        }
        return Vector2.zero;
    }

    private void ResetPlayerVelocity(Collider2D player)
    {
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 origin = transform.position + (Vector3)raycastOffset;
        Gizmos.DrawLine(origin, origin + Vector3.left * checkDistance);
        Gizmos.DrawLine(origin, origin + Vector3.right * checkDistance * 1.5f);
    }
}