using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[Serializable]
public class VFXHandler : MonoBehaviour
{

    [SerializeField] private VisualEffect SlashVFX1;
    [SerializeField] private VisualEffect SlashVFX2;
    [SerializeField] private VisualEffect SlashVFX3;    

    public void PlayVFX1()
    {       
        SlashVFX1.Play();
    }
    public void PlayVFX2()
    {
        SlashVFX2.Play();

    }
    public void PlayVFX3()
    {
        SlashVFX3.Play();
    }
}


    