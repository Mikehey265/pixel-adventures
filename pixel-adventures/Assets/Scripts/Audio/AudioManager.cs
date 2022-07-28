using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    #region Public Variables

    [SerializeField] public AudioClip[] enemySFX;
    [SerializeField] public AudioClip[] menuSFX;

    #endregion

    #region Private Variables

    [SerializeField] AudioSource audioSource;
    [SerializeField] float audioClipLength = 0.3f;

    #endregion

    #region Unity Methods

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    #endregion

    #region Public Methods

    public void PlayEnemySFX(AudioClip audioClip){
        audioSource.PlayOneShot(audioClip, audioClipLength);
    }

    public void PlayMenuSFX(AudioClip audioClip){
        audioSource.PlayOneShot(audioClip, audioClipLength);
    }

    #endregion
}
