using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("References")]
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public Toggle musicToggle;
    public AudioSource backgroundMusic;

    void Start()
    {
        musicToggle.isOn = backgroundMusic.enabled;

        musicToggle.onValueChanged.AddListener(ToggleMusic);
    }

    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ToggleMusic(bool isOn)
    {
        backgroundMusic.enabled = isOn;
        
        PlayerPrefs.SetInt("MusicEnabled", isOn ? 1 : 0);
    }
}