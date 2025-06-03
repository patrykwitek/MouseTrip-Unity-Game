using UnityEngine;

public class IceSurface : MonoBehaviour
{
    [Header("Ice Settings")]
    [SerializeField] private float slideFriction = 0.2f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerMovement>();
            if (player != null) player.SetIceFriction(slideFriction);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerMovement>();
            if (player != null) player.ResetIceFriction();
        }
    }
}