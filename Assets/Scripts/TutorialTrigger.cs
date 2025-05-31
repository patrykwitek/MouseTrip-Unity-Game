using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    [Header("Ustawienia")] public Sprite tutorialImage;
    public float freezeTimeScale = 0.05f;
    public KeyCode unlockKey = KeyCode.Space;

    [Header("Referencje")] public Image tutorialDisplay;
    public GameObject player;
    [SerializeField] private AudioClip messageSound;

    private bool isTutorialActive = false;
    private PlayerMovement playerMovement;
    private Rigidbody2D playerRigidbody;
    private Vector2 savedVelocity;
    private float savedAngularVelocity;

    private void Start()
    {
        tutorialDisplay.gameObject.SetActive(false);

        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            playerRigidbody = player.GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        if (isTutorialActive && Input.GetKeyDown(unlockKey))
        {
            UnfreezeGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTutorialActive)
        {
            FreezeGame();
        }
    }

    private void FreezeGame()
    {
        // Zapisanie aktualnej prędkości przed zatrzymaniem
        if (playerRigidbody != null)
        {
            savedVelocity = playerRigidbody.linearVelocity;
            savedAngularVelocity = playerRigidbody.angularVelocity;
            playerRigidbody.isKinematic = true;
        }

        // Zatrzymanie czasu i inputu
        Time.timeScale = freezeTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // ważne dla fizyki

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Wyświetlenie komunikatu
        tutorialDisplay.sprite = tutorialImage;
        tutorialDisplay.gameObject.SetActive(true);

        if (messageSound != null)
        {
            AudioSource.PlayClipAtPoint(messageSound, transform.position);
        }

        isTutorialActive = true;
    }

    private void UnfreezeGame()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        
        tutorialDisplay.gameObject.SetActive(false);
        
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

        isTutorialActive = false;
        
        gameObject.SetActive(false);
    }
    
    private void OnDisable()
    {
        if (isTutorialActive)
        {
            UnfreezeGame();
        }
    }
}