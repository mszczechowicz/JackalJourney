using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Health : MonoBehaviour , IJsonSaveable
{
    [SerializeField] private int maxHealth = 1000;
    public int healthPoints;

    private bool isInvulnerable;
    //--ImpactStateLogic komentujê do czas a¿ zaimplementujemy "HeavyAttack dla bossów"
    public event Action OnTakeDamage;
    

    public event Action OnDie;
    
    [HideInInspector] public UnityEvent onDie_UnityEvent;
 

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

        //true_healthSlider.maxValue = maxHealth;
        //true_healthSlider.minValue = 0;

        //easy_healthSlider.maxValue = maxHealth;
        //easy_healthSlider.minValue = 0;
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
       

        if (healthPoints == 0)
        {
          
            Debug.Log("healthPoints == 0");
            OnDie?.Invoke();
            onDie_UnityEvent.Invoke();
        }

        //Debug.Log(healthPoints);
    }
    public void CreatePopUp(int damage)
    {
        PopUpDamage.Create(this.transform.position, damage, Color.yellow);

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
