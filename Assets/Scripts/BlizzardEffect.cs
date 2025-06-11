using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlizzardEffect : MonoBehaviour
{
    [Header("Freezing settings")] [SerializeField]
    public float freezeTime = 48f;
    public float rechargeDuration = 10f; // Time to go from 0 to 10
    public Slider valueSlider;
    public HealthSystem healthSystem;

    [Header("Debug")]
    [SerializeField] private float currentValue = 10f;
    private bool isDraining = false;
    private bool isRecharging = false;
    private IEnumerator drainRoutine;
    private IEnumerator rechargeRoutine;
    
    private float damageCooldown = 5f;
    private float timeSinceLastDamage = 0f;

    
    void Start()
    {
        currentValue = 10f;
        UpdateSlider();
    }
    void FixedUpdate()
    {
        if (currentValue <= 0f) {
            timeSinceLastDamage += Time.deltaTime;
            
            if (timeSinceLastDamage >= damageCooldown)
            {
                healthSystem.TakeDamage(1);
                timeSinceLastDamage = 0f; // Reset timer
            }
        }

        if (isDraining && !isRecharging)
        {
            currentValue -= Time.fixedDeltaTime * (10f / freezeTime);
            currentValue = Mathf.Clamp(currentValue, 0f, 10f);
            UpdateSlider();
        
            if (currentValue <= 0f)
            {
                isDraining = false;
            }
        } 
        else if (isRecharging && !isDraining)
        {
            currentValue += Time.fixedDeltaTime * (10f / rechargeDuration);
            currentValue = Mathf.Clamp(currentValue, 0f, 10f);
            UpdateSlider();
        }
    }


    public void StartDraining()
    {
        isDraining = true;
        isRecharging = false;
    }
    public void StopDraining()
    {
        isDraining = false;
        isRecharging = true;
    }
    
    private void UpdateSlider()
    {
        valueSlider.value = currentValue;
    }

}
