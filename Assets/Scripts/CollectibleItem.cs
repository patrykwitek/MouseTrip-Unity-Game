using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PointsManager.Instance.CollectItem();
            
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

            Destroy(gameObject);
        }
    }
}