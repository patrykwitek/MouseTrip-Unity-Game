using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class ChangeToNight : MonoBehaviour
{
    [Header("Night settings")]
    [SerializeField] public float dayIntensity = 1f;
    [SerializeField] public float nightIntensity = 0.3f;
    [SerializeField] public float transitionDuration = 5f;
    public Light2D globalLight;
    public SpriteRenderer dayAsset;
    public SpriteRenderer nightAsset;
    private bool _isNight = false;
    public Tilemap foreground;
    public Tilemap background;
    public Color dayColor = Color.white;
    public Color nightColor = new Color(105, 105, 105);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_isNight == false)
            {
                StartCoroutine(TransitionAssets());
                StartCoroutine(TransitionTilemap());
                StartCoroutine(TransitionToNight());
            }
        }
    }
    

    // Update is called once per frame
    IEnumerator TransitionToNight()
    {
        float elapsedTime = 0f;
        float startIntensity = globalLight.intensity;
        
        while (elapsedTime < transitionDuration)
        {
            globalLight.intensity = Mathf.Lerp(startIntensity, nightIntensity, elapsedTime/transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        globalLight.intensity = nightIntensity;
    }
    
    IEnumerator TransitionAssets()
    {
        float elapsedTime = 0f;
        Color dayColor = dayAsset.color;
        Color nightColor = nightAsset.color;
        
        float startDayAlpha = dayColor.a;
        float startNightAlpha = nightColor.a;
        
        float targetDayAlpha = _isNight ? 1f : 0f;
        float targetNightAlpha = _isNight ? 0f : 1f;
        
        _isNight = !_isNight; // Toggle state for next time

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;
            
            // Smoothly interpolate alpha values
            dayColor.a = Mathf.Lerp(startDayAlpha, targetDayAlpha, t);
            nightColor.a = Mathf.Lerp(startNightAlpha, targetNightAlpha, t);
            
            dayAsset.color = dayColor;
            nightAsset.color = nightColor;
            
            yield return null;
        }

        // Ensure final values are set exactly
        dayColor.a = targetDayAlpha;
        nightColor.a = targetNightAlpha;
        dayAsset.color = dayColor;
        nightAsset.color = nightColor;
    }
    
    IEnumerator TransitionTilemap()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            foreground.color = Color.Lerp(dayColor, nightColor, t);
            background.color = Color.Lerp(dayColor, nightColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        foreground.color = nightColor;
        background.color = nightColor;
    }
}
