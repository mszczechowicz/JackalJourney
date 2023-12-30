using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] float maxStamina = 90;
    [SerializeField] float staminaRegenRate = 3f;

    float stamina;

    private void Awake()
    {
        stamina = maxStamina;
    }
    private void Update()
    {
        if(stamina< maxStamina )
        {
            stamina += staminaRegenRate * Time.deltaTime;
            if (stamina > maxStamina) { stamina = maxStamina; }
        }
    }
    public float GetStamina()
    {
        return stamina;
    }

    public float GetMaxStamina()
    {
        return maxStamina;
    }
    public bool DrainStamina(float staminaToUse)
    { 
        if(staminaToUse>stamina)
        {

            return false;
        }
        stamina -=staminaToUse;
        return true;
    }
}
