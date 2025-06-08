using UnityEngine;

public class DeactivateUI : MonoBehaviour
{
    [SerializeField] private Canvas canvasToShow;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasToShow.gameObject.SetActive(false);
        }
    }
}
