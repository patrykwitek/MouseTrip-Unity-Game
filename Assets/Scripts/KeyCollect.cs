using UnityEngine;
using UnityEngine.Serialization;

public class KeyCollect : MonoBehaviour
{
    [FormerlySerializedAs("keyPickupEffect")]
    [Header("Key Settings")]
    [SerializeField] private GameObject keys;
    [SerializeField] private AudioClip keyPickupSound;

    [FormerlySerializedAs("doorOpenEffect")]
    [Header("Door Settings")]
    [SerializeField] private GameObject doors;
    [SerializeField] private AudioClip doorOpenSound;

    private bool hasKey = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            PickupKey(other.gameObject);
        }
        else if (other.CompareTag("Door") && hasKey)
        {
            OpenDoor(other.gameObject);
        }
    }

    private void PickupKey(GameObject key)
    {
        hasKey = true;
        
        if (keys != null)
            Instantiate(keys, key.transform.position, Quaternion.identity);
        
        if (keyPickupSound != null)
            AudioSource.PlayClipAtPoint(keyPickupSound, transform.position);
        
        Destroy(key);
    }

    private void OpenDoor(GameObject door)
    {
        if (doorOpenSound != null)
            AudioSource.PlayClipAtPoint(doorOpenSound, transform.position);
        
        Destroy(doors);
        
        hasKey = false;
    }
}