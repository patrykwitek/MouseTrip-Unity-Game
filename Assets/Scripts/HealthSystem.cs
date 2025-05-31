using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] public int currentHealth;
    [SerializeField] public int maxHealth = 10;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    
    [Header("Death Settings")]
    [SerializeField] private GameObject objectToMoveOnDeath;
    [SerializeField] private Vector2 deathPosition = new Vector2(12f, 6f);
    [SerializeField] private float respawnDelay = 1f;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip healthDecreaseSound;
    
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            AudioSource.PlayClipAtPoint(healthDecreaseSound, Camera.main.transform.position);
        }
    }
    
    public void AddHeart()
    {
        currentHealth += 1;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
            hearts[i].enabled = i < maxHealth;
        }
    }

    private void Die()
    {
        if (deathSound is not null)
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);

        if (objectToMoveOnDeath is not null)
        {
            Invoke("MoveObject", respawnDelay);
        }
    }
    
    private void MoveObject()
    {
        objectToMoveOnDeath.transform.position = deathPosition;
        
        currentHealth = maxHealth;
        UpdateHearts();
    }
}