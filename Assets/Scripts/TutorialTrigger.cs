using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    [Header("Ustawienia")]
    public Sprite tutorialImage; // Przeciągnij tutaj swój PNG z instrukcją
    public float freezeTimeScale = 0.05f; // Wartość "zamrożenia" gry
    public KeyCode unlockKey = KeyCode.Space; // Klawisz do kontynuacji

    [Header("Referencje")]
    public Image tutorialDisplay; // UI Image do wyświetlania instrukcji
    public GameObject player; // Referencja do obiektu gracza

    private bool isTutorialActive = false;
    private PlayerMovement playerMovement; // Zakładając, że masz skrypt PlayerMovement

    private void Start()
    {
        // Wyłącz obraz na starcie
        tutorialDisplay.gameObject.SetActive(false);
        
        // Pobierz komponent PlayerMovement
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
        // Zamroź czas gry
        Time.timeScale = freezeTimeScale;
        
        // Wyświetl instrukcję
        tutorialDisplay.sprite = tutorialImage;
        tutorialDisplay.gameObject.SetActive(true);
        
        // Zablokuj ruch postaci
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        
        isTutorialActive = true;
    }

    private void UnfreezeGame()
    {
        // Przywróć normalny czas gry
        Time.timeScale = 1f;
        
        // Ukryj instrukcję
        tutorialDisplay.gameObject.SetActive(false);
        
        // Odblokuj ruch postaci
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
        
        isTutorialActive = false;
        
        // Opcjonalnie: deaktywuj ten trigger aby się nie powtarzał
        gameObject.SetActive(false);
    }
}