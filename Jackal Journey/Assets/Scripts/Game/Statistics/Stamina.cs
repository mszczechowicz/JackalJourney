using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] float maxStamina = 90;

    float stamina;

    private void Awake()
    {
        stamina = maxStamina;
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
