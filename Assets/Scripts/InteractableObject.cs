using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactionRange = 1f;
    [SerializeField] private float promptRange = 2f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private GameObject objectToDisappear;
    [SerializeField] private AudioClip interactionSound;
    [SerializeField] private float disappearDelay = 1f;
    
    [Header("Visual Feedback")]
    [SerializeField] private GameObject interactionPrompt;

    [Header("Texture Change")]
    [SerializeField] private bool changeTexture = false;
    [SerializeField] private Sprite newSprite;
    [SerializeField] private SpriteRenderer targetSpriteRenderer;
    
    [Header("Temporary Object")]
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private float activateDuration = 3f;
    
    private Transform player;
    private bool isInRange = false;
    private bool hasInteracted = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;
        
        float distance = Vector3.Distance(transform.position, player.position);
        bool nowInRange = distance <= interactionRange;

        if (nowInRange != isInRange)
        {
            isInRange = nowInRange;
        }
        
        if (distance <= promptRange)
        { 
            interactionPrompt.SetActive(false);
        }
        else
        {
            interactionPrompt.SetActive(true);
        }
        
        if (isInRange && Input.GetKeyDown(interactionKey))
        {
            Interact();
        }
    }

    void Interact()
    {
        hasInteracted = true;
        
        if (objectToDisappear != null)
        {
            StartCoroutine(DisappearWithDelay(objectToDisappear, disappearDelay));
        }
        
        if (objectToActivate != null)
        {
            StartCoroutine(ActivateTemporarily(objectToActivate, activateDuration));
        }

        if (changeTexture && newSprite != null && targetSpriteRenderer != null)
        {
            targetSpriteRenderer.sprite = newSprite;
        }
        
        if (interactionSound != null)
        AudioSource.PlayClipAtPoint(interactionSound, transform.position);
        
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
        
        enabled = false;
    }
    
    IEnumerator DisappearWithDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
            
        obj.SetActive(false);
    }
    
    IEnumerator ActivateTemporarily(GameObject obj, float duration)
    {
        obj.SetActive(true);
        
        yield return new WaitForSeconds(duration);
        
        obj.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}