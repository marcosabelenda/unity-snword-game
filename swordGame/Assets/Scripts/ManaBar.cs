using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManaBar : MonoBehaviour
{

    private Image barImage;

    public Mana mana;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        mana = new Mana();
    }

    // Update is called once per frame
    void Update()
    {
        mana.Update();
        barImage.fillAmount = mana.GetManaNormalized();
    }
}

public class Mana{
    public const int MANA_MAX = 100;

    private float manaAmount;
    private float manaRegenAmount;

    public Mana(){
        manaAmount = 100;
        manaRegenAmount = 10f;
    }

    public void Update(){
        if (manaAmount < MANA_MAX)
        {
            manaAmount += manaRegenAmount * Time.deltaTime;
        }
    }

    public bool SpendMana(int amount){
        if (manaAmount >= amount)
        {
            manaAmount -= amount;
            return true;
        }

        return false;
    }

    public float GetManaNormalized(){
        return manaAmount / MANA_MAX;
    }
    
}
