using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour , IJsonSaveable
{
    [SerializeField] private int maxHealth = 1000;
    [SerializeField] private Slider true_healthSlider;
    [SerializeField] private Slider easy_healthSlider;
    [SerializeField] public int healthPoints;
    private float easylerpingintoHealth = 0.01f;

   
    private bool isInvulnerable;
    //--ImpactStateLogic komentujê do czas a¿ zaimplementujemy "HeavyAttack dla bossów"
    public event Action OnTakeDamage;
    
    public event Action OnDie;
    
    [HideInInspector] public UnityEvent onDie_UnityEvent;


    private void Update()
    {
        //do przeniesienia te wartoœci tam gdzie bedzie zadawane obra¿enie a nie w updacie wszystko na si³ê robione
        true_healthSlider.value = healthPoints;
        

        if (true_healthSlider.value != easy_healthSlider.value)
        {
            easy_healthSlider.value = Mathf.Lerp(easy_healthSlider.value, true_healthSlider.value, easylerpingintoHealth);
           
        }
    }

    public bool IsDead => GetHealthPoints() == 0;


    public float GetHealthPoints()
    {
        return healthPoints;
    }
    public float GetMaxHealthPoints()
    {
        return maxHealth;
    }


    private void Awake()
    {
       if(healthPoints == 0)
            OnDie?.Invoke();


        healthPoints = maxHealth;

        true_healthSlider.maxValue = maxHealth;
        true_healthSlider.minValue = 0;

        easy_healthSlider.maxValue = maxHealth;
        easy_healthSlider.minValue = 0;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }



    public void TakeDamage(int damagevalue)
    {
        if (healthPoints == 0)  {return;}
        if (isInvulnerable)     {return;}

        healthPoints = Mathf.Max(healthPoints - damagevalue, 0);
        Debug.Log("TakeDamage()");
        //--ImpactStateLogic komentujê do czas a¿ zaimplementujemy "HeavyAttack dla bossów"
        OnTakeDamage?.Invoke();
       
        if(healthPoints == 0)
        {
          
            Debug.Log("healthPoints == 0");
            OnDie?.Invoke();
            onDie_UnityEvent.Invoke();
        }

        Debug.Log(healthPoints);
    }
    public JToken CaptureAsJToken()
    {
        return JToken.FromObject(healthPoints);
    }

    public void RestoreFromJToken(JToken state)
    {
        healthPoints = state.ToObject<int>();
        if(healthPoints == 0)
        {
            OnDie?.Invoke();

        }
        
    }
}
