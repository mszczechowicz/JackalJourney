using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioRandomizer : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSource;
    [Range(0.1f, 0.5f)]
    public float volumeChangeMulitplier = 0.2f;
    public float pitchChangeMulptiplier = 0.2f;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        RandomizeSounds();
    }

    public void RandomizeSounds()
    {
        if (!audioSource.isPlaying)  
        {
            audioSource.clip = sounds[Random.Range(0, sounds.Length)];
            audioSource.volume = Random.Range(1 - volumeChangeMulitplier, 1);
            audioSource.pitch = Random.Range(1 - pitchChangeMulptiplier, 1 + pitchChangeMulptiplier);
            
        }

    }
}