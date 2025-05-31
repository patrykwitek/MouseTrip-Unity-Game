using UnityEngine;
using UnityEngine.UI;

public class ActivateUIObject : MonoBehaviour
{
    [SerializeField] private Canvas canvasToShow;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasToShow.gameObject.SetActive(true);
        }
    }
}
