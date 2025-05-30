using UnityEngine;
using UnityEngine.UI;

public class ShowPointsTrigger : MonoBehaviour
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
    
    [SerializeField] private Canvas canvasToShow;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasToShow.gameObject.SetActive(true);
        }
    }
    
    private void FreezeGame()
    {
        Time.timeScale = freezeTimeScale;
        
        tutorialDisplay.sprite = tutorialImage;
        tutorialDisplay.gameObject.SetActive(true);
        
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
