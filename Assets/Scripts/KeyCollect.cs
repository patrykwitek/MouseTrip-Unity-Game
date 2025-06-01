using UnityEngine;

public class KeyCollect : MonoBehaviour
{
    [SerializeField] private int keyID;
    [SerializeField] private AudioClip collectKeysSound;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(collectKeysSound, transform.position);
            
            other.GetComponent<PlayerInventory>().AddKey(keyID);
            
            Destroy(gameObject);
        }
    }
}