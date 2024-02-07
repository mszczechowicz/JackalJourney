using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Health Health;
    [SerializeField] GameObject HealtbarParentUI;
    [SerializeField] private Slider true_healthSlider;
    [SerializeField] private Slider easy_healthSlider;

  

    void Awake()
    {

        true_healthSlider.maxValue = Health.GetMaxHealthPoints();
        true_healthSlider.minValue = 0;

        easy_healthSlider.maxValue = Health.GetMaxHealthPoints();
        easy_healthSlider.minValue = 0;

        if (Health.GetHealthPoints() == Health.GetMaxHealthPoints())
        {
           transform.GetChild(0).gameObject.SetActive(false);
            return;
        }
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

        HealtbarParentUI.SetActive(true);

        if (Health.GetHealthPoints()<=0)
        {
            DeactivateHealthBar();
           
            return;
        }

    }

    private void Update()
    {
        //do przeniesienia te wartoœci tam gdzie bedzie zadawane obra¿enie a nie w updacie wszystko na si³ê robione
        true_healthSlider.value = Health.GetHealthPoints(); ;


        if (true_healthSlider.value != easy_healthSlider.value)
        {
            easy_healthSlider.value = Mathf.Lerp(easy_healthSlider.value, true_healthSlider.value, 0.01f);

        }
    }

    void DeactivateHealthBar()
    {
        HealtbarParentUI.SetActive(false);

    }


}

