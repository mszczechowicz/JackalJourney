using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpDamage : MonoBehaviour
{
    // to set up in placec where enemy is hit add : PopUpDamage.Create(enemyHandler.GetPosition(), damageAmount);
    // Create dmg pop up
    // another way is object pooling ( hidden empty objects with pop ups that will be teleported to enemy and shown after hit and then dissapearing to empty again )
    // to upgrade animation we can add AnimationCurves instead of valuues for Opacity,Scale and height, color to customization etc
    // public AnimationCurve opacityCurve
    // public AnimationCurve scaleCurve
    // public AnimationCurve heightCurve

    /*if needed for always facing camera private Camera cam;
    private void Awake()
    {

        cam = Camera.main;
    }

    private void update()
    {
        transform.forward = cam.transform.forward;
    }

    */
    public static  PopUpDamage Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.DamageIndicator, position, Quaternion.identity);
        PopUpDamage damagePopup = damagePopupTransform.GetComponent<PopUpDamage>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    private static int sortingOrder;
    private const float MAX_DISAPPEAR = 1f;
    private TextMeshPro textMesh;
    private float dissapearTimer;
    private Color textcolor;
    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }
    public void Setup(int damageAmount)
    {
        // basic setup
        textMesh.SetText(damageAmount.ToString());
        textcolor = textMesh.color;
        dissapearTimer = MAX_DISAPPEAR;
        moveVector = new Vector3(1, 1) * 30f;
        sortingOrder++;
        textMesh.sortingOrder= sortingOrder;
    }

    private void Update()
    {
        // move pop up
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;
        // change scale in time
        if (dissapearTimer > MAX_DISAPPEAR * 0.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;

        } else{

            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;

        }

        // dissapear after time
        dissapearTimer -= Time.deltaTime;
        if(dissapearTimer < 0)
        {
            Debug.Log("start dissapear");
            float dissapearSpeed = 3f;
            textcolor.a -= dissapearSpeed* Time.deltaTime;
            textMesh.color = textcolor;

            if(textcolor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
