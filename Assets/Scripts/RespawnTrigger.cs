using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private bool resetVelocity = true;
    [SerializeField] private HealthSystem healthSystem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.gameObject);
            healthSystem.TakeDamage(1);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        SetPlayerComponents(player, false);

        Vector2 targetPosition = respawnPoint.position;

        player.transform.position = targetPosition;

        SetPlayerComponents(player, true);
    }

    private void SetPlayerComponents(GameObject player, bool state)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            if (resetVelocity && !state)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            rb.simulated = state;
        }

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = state;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector2 targetPos = respawnPoint.position;
        Gizmos.DrawWireSphere(targetPos, 0.5f);
        Gizmos.DrawLine(transform.position, targetPos);
    }
}