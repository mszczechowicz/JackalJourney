using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health Health;
    [SerializeField] private Slider true_healthSlider;
    [SerializeField] private Slider easy_healthSlider;
    [SerializeField] Stamina Stamina;
    [SerializeField] private Slider true_staminaSlider;


    void Awake()
    {
        true_staminaSlider.maxValue = Stamina.GetMaxStamina();
        true_staminaSlider.minValue = 0;

        true_healthSlider.maxValue = Health.GetMaxHealthPoints();
        true_healthSlider.minValue = 0;

        easy_healthSlider.maxValue = Health.GetMaxHealthPoints();
        easy_healthSlider.minValue = 0;
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += UpdateHealthBar;
    }
    private void OnDisable()
    {
        Health.OnTakeDamage -= UpdateHealthBar;
    }

    public void UpdateHealthBar()
    {
        //true_healthSlider.value = Health.GetHealthPoints() ;


        //if (true_healthSlider.value != easy_healthSlider.value)
        //{
        //    easy_healthSlider.value = Mathf.Lerp(easy_healthSlider.value, true_healthSlider.value, 0.01f);

        //}

    }

    private void Update()
    {
        //do przeniesienia te warto�ci tam gdzie bedzie zadawane obra�enie a nie w updacie wszystko na si�� robione
        true_healthSlider.value = Health.GetHealthPoints(); ;
        true_staminaSlider.value = Stamina.GetStamina();
        if (Stamina.GetStamina() < 30)
        {
            true_staminaSlider.fillRect.gameObject.GetComponent<Image>().color = Color.grey;

        }
        else
        {
            true_staminaSlider.fillRect.gameObject.GetComponent<Image>().color = new Color(0.9803f, 0.698f, 0.031f);
        }
       

        if (true_healthSlider.value != easy_healthSlider.value)
        {
            easy_healthSlider.value = Mathf.Lerp(easy_healthSlider.value, true_healthSlider.value, 0.01f);

        }
    }

   


}
