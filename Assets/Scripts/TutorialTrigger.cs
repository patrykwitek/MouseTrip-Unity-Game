using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    [Header("Ustawienia")]
    public Sprite tutorialImage;
    public float freezeTimeScale = 0.05f;
    public KeyCode unlockKey = KeyCode.Space;

    [Header("Referencje")]
    public Image tutorialDisplay;
    public GameObject player;

    private bool isTutorialActive = false;
    private PlayerMovement playerMovement;

    [SerializeField] private AudioClip messageSound;
    
    private void Start()
    {
        tutorialDisplay.gameObject.SetActive(false);
        
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
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
        Time.timeScale = freezeTimeScale;
        
        tutorialDisplay.sprite = tutorialImage;
        tutorialDisplay.gameObject.SetActive(true);
        
        AudioSource.PlayClipAtPoint(messageSound, transform.position);
        
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        
        isTutorialActive = true;
    }

    private void UnfreezeGame()
    {
        Time.timeScale = 1f;

        tutorialDisplay.gameObject.SetActive(false);

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
        
        isTutorialActive = false;
        
        gameObject.SetActive(false);
    }
}