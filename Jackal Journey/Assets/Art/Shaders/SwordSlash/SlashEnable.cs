using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class SlashEnable : MonoBehaviour
{

    [SerializeField] private VisualEffect SlashVFX;

    public void OnSlash()
    {
        
        PlayParticle();

        
    }

    private void PlayParticle()
    {
        
        SlashVFX.Play();
        Debug.Log("Tak");
        
    }
}


    