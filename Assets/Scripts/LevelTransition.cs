using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject transitionPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private AudioClip messageSound;
    
    [Header("Scene Names")]
    [SerializeField] private string menuSceneName = "Menu";
    [SerializeField] private string currentLevelName = "Level1";
    
    private PointsManager pointsManager;
    private void Start()
    {
        if (transitionPanel != null)
            transitionPanel.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartLevel);

        if (menuButton != null)
            menuButton.onClick.AddListener(ReturnToMenu);
        
        pointsManager = FindObjectOfType<PointsManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (messageSound != null)
            {
                AudioSource.PlayClipAtPoint(messageSound, transform.position);
            }
            
            ShowTransitionPanel();
        }
    }

    private void ShowTransitionPanel()
    {
        Time.timeScale = 0f;
        if (transitionPanel != null)
            transitionPanel.SetActive(true);
        
        if (pointsText != null)
        {
            if (pointsManager != null)
            {
                pointsText.text = pointsManager.collectedCount.ToString() + " / " + pointsManager.totalCollectibles.ToString();
                pointsManager.ResetCounter();
            }
        }

        if (restartButton != null)
            restartButton.interactable = true;
        
        if (menuButton != null)
            menuButton.interactable = true;
    }

    private void RestartLevel()
    {
        pointsManager.ResetCounter();
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentLevelName);
    }

    private void ReturnToMenu()
    {
        pointsManager.ResetCounter();
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}