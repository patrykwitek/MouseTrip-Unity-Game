using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OpenBook : MonoBehaviour
{
    public Sprite bookImage;
    public float freezeTimeScale = 0.05f;
    public KeyCode unlockKey = KeyCode.Q;
    public KeyCode interactionKey = KeyCode.E;
    private bool isBookActive = false;
    [SerializeField] private float promptRange = 2f;
    
    public Image bookImageDisplay;
    public GameObject player;
    [SerializeField] private AudioClip messageSound;

    private PlayerMovement playerMovement;
    private Rigidbody2D playerRigidbody;
    private Vector2 savedVelocity;
    private float savedAngularVelocity;
    
    [SerializeField] private GameObject interactionPrompt;
    
    private Transform playerTransform;

    private void Start()
    {
        bookImageDisplay.gameObject.SetActive(false);

        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            playerRigidbody = player.GetComponent<Rigidbody2D>();
        }
        
        playerTransform = player.transform;
    }

    private void Update()
    {
        if (isBookActive && Input.GetKeyDown(unlockKey))
        {
            UnfreezeGame();
        }
        
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        
        if (distance <= promptRange)
        { 
            interactionPrompt.SetActive(false);
        }
        else
        {
            interactionPrompt.SetActive(true);
        }

        if (distance <= 1f && Input.GetKeyDown(interactionKey))
        {
            FreezeGame();
        }
    }

    private void FreezeGame()
    {
        if (playerRigidbody != null)
        {
            savedVelocity = playerRigidbody.linearVelocity;
            savedAngularVelocity = playerRigidbody.angularVelocity;
            playerRigidbody.isKinematic = true;
        }
        
        Time.timeScale = freezeTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        
        bookImageDisplay.sprite = bookImage;
        bookImageDisplay.gameObject.SetActive(true);

        if (messageSound != null)
        {
            AudioSource.PlayClipAtPoint(messageSound, transform.position);
        }

        isBookActive = true;
    }

    private void UnfreezeGame()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        
        bookImageDisplay.gameObject.SetActive(false);
        
        if (playerRigidbody != null)
        {
            playerRigidbody.isKinematic = false;
            playerRigidbody.linearVelocity = savedVelocity;
            playerRigidbody.angularVelocity = savedAngularVelocity;
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        isBookActive = false;
    }
    
    private void OnDisable()
    {
        if (isBookActive)
        {
            UnfreezeGame();
        }
    }
}