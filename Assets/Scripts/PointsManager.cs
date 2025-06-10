using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance;
    
    [SerializeField] private TextMeshProUGUI  counterText;
    public int totalCollectibles;
    public int collectedCount;
    

    private void Awake()
    {
        // DontDestroyOnLoad(this.gameObject);
        if (Instance is null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        totalCollectibles = GameObject.FindGameObjectsWithTag("Cheese").Length;
        UpdateCounter();
    }
    
    public void CollectItem()
    {
        collectedCount += 1;
        UpdateCounter();
    }

    public void AddToTotalCollectibles(int amount)
    {
        totalCollectibles += amount;
        UpdateCounter();
    }

    public void ResetCounter()
    {
        collectedCount = 0;
    }
    
    private void UpdateCounter()
    { 
        counterText.text = $"{collectedCount} / {totalCollectibles}";
    }
}
