using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] public AudioClip[] enemySFX;
    [SerializeField] float audioClipLength = 0.3f;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayEnemySFX(AudioClip audioClip){
        audioSource.PlayOneShot(audioClip, audioClipLength);
    }
}
