using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class AudioRandomizer : MonoBehaviour
    {
        [SerializeField] float lowerPitchRange = 0.8f;
        [SerializeField] float upperPitchRange = 1.2f;
        [SerializeField] float volumeChangeMulitplier = 0.2f;
        [SerializeField] AudioClip[] soundSet = null;
        AudioSource audioSource = null;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayRandomisedSound()
        {
            // Set the clip to be a random one from the set
            audioSource.clip = soundSet[Mathf.RoundToInt(Random.Range(0, soundSet.Length - 1))];
            audioSource.volume = Random.Range(1 - volumeChangeMulitplier, 1);
            // Set the pitch to a random value within the specified range.
            audioSource.pitch = Random.Range(lowerPitchRange, upperPitchRange);

            audioSource.Play();
        }
    }
