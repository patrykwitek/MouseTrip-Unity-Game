using UnityEngine;

public class HeartCollectible : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AudioClip collectSound;

    [Header("References")]
    [SerializeField] private HealthSystem playerHealthSystem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TryCollectHeart();
        }
    }

    private void TryCollectHeart()
    {
        if (playerHealthSystem.currentHealth < playerHealthSystem.maxHealth)
        {
            playerHealthSystem.AddHeart();

            if (collectSound is not null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            
            Destroy(gameObject);
        }
    }
}