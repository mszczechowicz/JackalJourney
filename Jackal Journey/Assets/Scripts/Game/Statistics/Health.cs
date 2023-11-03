using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1000;
    [SerializeField] private Slider true_healthSlider;
    [SerializeField] private Slider easy_healthSlider;
    private float easylerpingintoHealth = 0.01f;

    private int health;
    private bool isInvulnerable;
    //--ImpactStateLogic komentujê do czas a¿ zaimplementujemy "HeavyAttack dla bossów"
    public event Action OnTakeDamage;
    
    public event Action OnDie;

    
    private void Update()
    {
        //do przeniesienia te wartoœci tam gdzie bedzie zadawane obra¿enie a nie w updacie wszystko na si³ê robione
        true_healthSlider.value = health;
        

        if (true_healthSlider.value != easy_healthSlider.value)
        {
            easy_healthSlider.value = Mathf.Lerp(easy_healthSlider.value, true_healthSlider.value, easylerpingintoHealth);
           
        }
    }

    public bool IsDead => health == 0;
    private void Start()
    {
        

        health = maxHealth;
        true_healthSlider.maxValue = maxHealth;
        true_healthSlider.minValue = 0;

        easy_healthSlider.maxValue = maxHealth;
        easy_healthSlider.minValue = 0;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }



    public void DealDamage(int damagevalue)
    {
        if (health == 0){    return; }

        if (isInvulnerable) { return; }

       health = Mathf.Max(health - damagevalue, 0);
        //--ImpactStateLogic komentujê do czas a¿ zaimplementujemy "HeavyAttack dla bossów"
        OnTakeDamage?.Invoke();
       
        if(health == 0)
        { OnDie?.Invoke(); }

        Debug.Log(health);
    }
}
