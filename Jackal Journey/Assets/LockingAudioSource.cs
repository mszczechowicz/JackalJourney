using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockingAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    public static bool isPlaying = false;

    public void PlayClip()
    {
        if (isPlaying) return;
        isPlaying = true;
        source.Play();
        StartCoroutine(ClearWhenClipPlayed());
       
    }

    private IEnumerator ClearWhenClipPlayed()
    {
        while (source.isPlaying)
        {
            yield return null;
           
        }
        isPlaying = false;
    }

}
