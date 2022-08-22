using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    #region Public Variables

    [SerializeField] public AudioClip[] enemySFX;
    [SerializeField] public AudioClip[] menuSFX;
    [SerializeField] public AudioClip[] destructionSFX;
    [SerializeField] public float audioVolumeScale = 0.3f;

    #endregion

    #region Private Variables

    [SerializeField] AudioClip explosionSFX;
    [SerializeField] AudioClip attackSFX; 
    [SerializeField] AudioSource audioSource;

    #endregion

    #region Unity Methods

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    #endregion

    #region Public Methods

    public void PlayEnemySFX(AudioClip audioClip){
        audioSource.PlayOneShot(audioClip, audioVolumeScale);
    }

    public void PlayMenuSFX(AudioClip audioClip){
        audioSource.PlayOneShot(audioClip, audioVolumeScale);
    }

    public void PlayDestructionSFX(AudioClip audioClip){
        audioSource.PlayOneShot(audioClip, audioVolumeScale);
    }

    public void PlayExplosionSFX(){
        audioSource.PlayOneShot(explosionSFX, audioVolumeScale);
    }

    public void PlayAttackSFX(){
        audioSource.PlayOneShot(attackSFX, audioVolumeScale);
    }

    #endregion
}
