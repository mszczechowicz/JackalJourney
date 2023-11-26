using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class SlashEnable : MonoBehaviour
{

    [SerializeField] private VisualEffect SlashVFX;
    [SerializeField] private ParticleSystem SlashParticle;

    //public void OnSlash()
    //{

    //    PlayParticle();



    //}
    private void Start()
    {
        SlashParticle.Stop();
    }
    public void OffSlash()
    {
        SlashVFX.Stop();
        SlashParticle.Stop();
    }

    public void PlayParticle()
    {
        
        SlashVFX.Play();
        SlashParticle.Play();

    }
}


    