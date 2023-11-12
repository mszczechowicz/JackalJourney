using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    CanvasGroup canvasGroup;

    private void Start()
    {
       canvasGroup = GetComponent<CanvasGroup>();
      
    }
    public void FadeOutImmediate()
    {
        canvasGroup.alpha = 1;
    }
    public IEnumerator FadeOutIn()
    {     
        yield return FadeOut(2f);
        print("Faded out");
        yield return FadeIn(2f);
        print("Faded in");
    }

    public IEnumerator FadeIn(float time)
    {
        Debug.Log("FadeIn");
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FadeOut(float time)
    {
        Debug.Log("FadeOut");
        while (canvasGroup.alpha < 1) 
        {
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }
    
    }
}
