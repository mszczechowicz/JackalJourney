using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpDamage : MonoBehaviour
{
    // to set up in placec where enemy is hit add : PopUpDamage.Create(enemyHandler.GetPosition(), damageAmount);
    // another way is object pooling ( hidden empty objects with pop ups that will be teleported to enemy and shown after hit and then dissapearing to empty again )
    public AnimationCurve opacityCurve;
    public AnimationCurve scaleCurve;
    public AnimationCurve heightCurve;
  
    private static int sortingOrder;
    private TextMeshPro textMesh;
    private Color textcolor;
    private Vector3 moveVector;
    private Vector3 initialScale;

    private float time = 0;
    private const float MAX_LIFE_TIME = 1f; // ustawienie zale¿y od d³ugoœci ¿ycia krzywej
    private float ScaleMultiplier;




    private void Awake()
    {
        ScaleMultiplier = Random.Range(1f, 3f);
        textMesh = GetComponent<TextMeshPro>();
        
    }
    public void Setup(int damageAmount, Color color)
    {
        // basic setup

        textMesh.SetText(damageAmount.ToString());
        textMesh.faceColor = color;
        moveVector = transform.position;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        initialScale = Vector3.one * ScaleMultiplier;
        transform.localScale = initialScale;
    }




    private void Update()
    {

        // zanikanie
        float alpha = opacityCurve.Evaluate(time);
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, alpha);




        // skalowanie pop up
        Vector3 scale = initialScale * scaleCurve.Evaluate(time);
        transform.localScale = scale;

        //zmiana pozycji
        transform.position = moveVector + new Vector3(0,1 + heightCurve.Evaluate(time), 0);


        // usuñ obiekt kiedy krzywa dobiegnie koñca
        time+= Time.deltaTime;

            if(time > MAX_LIFE_TIME)
            {
                Destroy(gameObject);
            }
        }
    


    public static PopUpDamage Create(Vector3 position, int damageAmount, Color color)
    {
        //ró¿ny punkt spawnowania pop up
        Vector3 randomness = new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f));


        Transform damagePopupTransform = Instantiate(GameAssets.instance.DamageIndicator, position + randomness, Quaternion.identity);
        PopUpDamage damagePopup = damagePopupTransform.GetComponent<PopUpDamage>();
        damagePopup.Setup(damageAmount, color);
        



        return damagePopup;
    }


}
