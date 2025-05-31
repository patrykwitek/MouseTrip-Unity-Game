using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoomLightController : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private float darkRoomIntensity = 0.1f;
    [SerializeField] private float transitionSpeed = 2f;
    
    [Header("References")]
    [SerializeField] private Light2D globalLight;
    
    private float defaultIntensity;
    private bool isInDarkRoom = false;

    private void Start()
    {
        if (globalLight != null)
        {
            defaultIntensity = globalLight.intensity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInDarkRoom = !isInDarkRoom;
        }
    }

    private void Update()
    {
        float targetIntensity = isInDarkRoom ? darkRoomIntensity : defaultIntensity;
        globalLight.intensity = Mathf.Lerp(globalLight.intensity, targetIntensity, Time.deltaTime * transitionSpeed);
    }
}