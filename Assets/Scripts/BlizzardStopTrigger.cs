using UnityEngine;

public class BlizzardStopTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public BlizzardEffect blizzardEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            blizzardEffect.StopDraining();
        }
    }
}
