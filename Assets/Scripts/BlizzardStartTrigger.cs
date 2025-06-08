using UnityEngine;

public class BlizzardStartTrigger : MonoBehaviour
{
    public BlizzardEffect blizzardEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            blizzardEffect.StartDraining();
        }
    }
}
