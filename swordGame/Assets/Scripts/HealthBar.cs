using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using CodeMonkey;

public class HealthBar : MonoBehaviour
{

    private const float DAMAGE_HEALTH_TIMER_MAX = 1f;
    private Image barImage;
    private Image damageBarImage;
    private float damageHealthTimer;
    private HealthSystem healthSystem;
    
    private void Awake() {
       barImage = transform.Find("Bar").GetComponent<Image>();
       damageBarImage = transform.Find("DamageBar").GetComponent<Image>();
    }

    private void Start()
    {
        healthSystem = new HealthSystem(100f);
        SetHealth(healthSystem.GetHealthNormalized());
        damageBarImage.fillAmount = barImage.fillAmount;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        damageHealthTimer = DAMAGE_HEALTH_TIMER_MAX;
        SetHealth(healthSystem.GetHealthNormalized()); 
    }
    
    private void SetHealth(float healthNormalized){
        barImage.fillAmount = healthNormalized;
    }
    
    public void LoseHealth(float amount){
        healthSystem.LoseHealth(amount);
        damageHealthTimer -= Time.deltaTime;
        if (damageHealthTimer < 0){
            if (barImage.fillAmount < damageBarImage.fillAmount)
            {
                const float speed = 1f;
                damageBarImage.fillAmount -= speed * Time.deltaTime;
            }
        }
    }
    

}

