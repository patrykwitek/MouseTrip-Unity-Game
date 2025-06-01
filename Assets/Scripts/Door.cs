using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int requiredKeyID;
    [SerializeField] private AudioClip openDoorsSound;
    
    private bool isOpened = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpened || !other.CompareTag("Player")) return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        
        if (inventory.HasKey(requiredKeyID))
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        AudioSource.PlayClipAtPoint(openDoorsSound, transform.position);
        isOpened = true;
        gameObject.SetActive(false);
    }
}