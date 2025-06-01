using UnityEngine;

public class AreaSoundController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private AudioSource fireAudioSource;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        
        float normalizedDistance = Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));

        float volume = 1f - normalizedDistance;

        fireAudioSource.volume = Mathf.Lerp(fireAudioSource.volume, volume, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}