using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private Image barImage;

    private Stamina stamina;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        stamina = new Stamina();
        barImage.fillAmount = stamina.GetStaminaNormalized();
    }

    // Update is called once per frame
    public void SpendStamina(float amount){
        stamina.SpendStamina(amount);
        barImage.fillAmount = stamina.GetStaminaNormalized();
    }

    public void RegenStamina(float amount){
        stamina.RegenStamina(amount);
        barImage.fillAmount = stamina.GetStaminaNormalized();
    }
    
}

public class Stamina{
    public const float STAMINA_MAX = 2f;

    private float staminaAmount;
  

    public Stamina(){
        staminaAmount = 0;
    }

  
    public void SpendStamina(float amount){
        if (staminaAmount >= amount)
        {
            staminaAmount -= amount;
        }
    }

    public void RegenStamina(float amount)
    {
        if (staminaAmount < STAMINA_MAX)
        {
            staminaAmount += amount;
        }
    }
    
    public float GetStaminaNormalized(){
        return staminaAmount / STAMINA_MAX;
    }

}
