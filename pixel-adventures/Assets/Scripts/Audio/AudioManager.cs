using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] public AudioClip[] enemySFX;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayEnemySFX(AudioClip audioClip){
        audioSource.PlayOneShot(audioClip, 0.3f);
    }
}
