
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;

    private float healthAmount;
    private float heathAmountMax;
 
    public HealthSystem(float healthAmount) {
        this.heathAmountMax = healthAmount;
        this.healthAmount = healthAmount;
        
    }

    public void LoseHealth(float amount)
    {
        healthAmount -= amount;
        if (healthAmount < 0)
        {
            healthAmount = 0;
        }
        if(OnDamaged != null) OnDamaged (this, EventArgs.Empty);
    }
    
    public float GetHealthNormalized() {
        return healthAmount / heathAmountMax;
    }

}
