using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

[Serializable]
public class VFXHandler : MonoBehaviour
{

    [SerializeField] private VisualEffect SlashVFX1;
    [SerializeField] private VisualEffect SlashVFX2;
    [SerializeField] private VisualEffect SlashVFX3;
    [SerializeField] private UnityEvent SlashSound;

    public void PlayVFX1()
    {
        SlashSound.Invoke();
        SlashVFX1.Play();
    }
    public void PlayVFX2()
    {
        SlashSound.Invoke();
        SlashVFX2.Play();

    }
    public void PlayVFX3()
    {
        SlashSound.Invoke();
        SlashVFX3.Play();
    }
}


    