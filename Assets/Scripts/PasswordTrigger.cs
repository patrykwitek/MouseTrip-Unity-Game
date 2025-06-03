using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordTrigger : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject passwordPanel;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Password Settings")]
    [SerializeField] private string correctPassword = "ch33s3";
    [SerializeField] private float interactionDistance = 2f;

    [Header("Object To Disappear")]
    [SerializeField] private GameObject objectToDisable;

    private Transform player;
    private bool isInRange = false;

    private PlayerMovement playerMovement;
    private Rigidbody2D playerRigidbody;
    private Vector2 savedVelocity;
    private float savedAngularVelocity;
    public float freezeTimeScale = 0.05f;
    [SerializeField] private AudioClip messageSound;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (passwordPanel != null) passwordPanel.SetActive(false);
        if (submitButton != null) submitButton.onClick.AddListener(CheckPassword);
    }

    void Update()
    {
        if (player == null) return;
        
        float distance = Vector3.Distance(transform.position, player.position);
        bool nowInRange = distance <= interactionDistance;
        
        if (nowInRange != isInRange)
        {
            isInRange = nowInRange;
            
            if (isInRange && passwordPanel != null)
            {
                passwordPanel.SetActive(true);
                FreezeGame();
            }
            else if (passwordPanel != null)
            {
                passwordPanel.SetActive(false);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && passwordPanel.activeSelf)
        {
            passwordPanel.SetActive(false);
            UnfreezeGame();
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
    }

    void CheckPassword()
    {
        if (passwordInputField == null) return;

        string enteredPassword = passwordInputField.text;
        
        if (enteredPassword == correctPassword)
        {
            if (messageText != null)
                messageText.text = "Poprawne hasło!";
            
            if (objectToDisable != null)
                objectToDisable.SetActive(false);
            
            // if (messageSound != null)
            // {
            //     AudioSource.PlayClipAtPoint(messageSound, transform.position);
            // }
            
            Invoke("DisablePanel", 1f);
            UnfreezeGame();
        }
        else
        {
            if (messageText != null)
                messageText.text = "Niepoprawne hasło!";
            
            passwordInputField.text = "";
            passwordInputField.Select();
        }
    }
    
    private void UnfreezeGame()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        
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
    }

    void DisablePanel()
    {
        if (passwordPanel != null)
            passwordPanel.SetActive(false);
        
        enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}