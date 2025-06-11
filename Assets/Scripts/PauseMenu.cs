using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    private bool isPaused = false;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    
    [Header("Scene Names")]
    [SerializeField] private string menuSceneName = "Menu";
    [SerializeField] private string currentLevelName = "Level1";

    private PointsManager pointsManager;
    private void Start()
    {
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartLevel);

        if (menuButton != null)
            menuButton.onClick.AddListener(ReturnToMenu);
        
        pointsManager = FindObjectOfType<PointsManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
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
