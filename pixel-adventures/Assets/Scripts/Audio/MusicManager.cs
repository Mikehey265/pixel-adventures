using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] musicClips;
    [SerializeField] float musicDelay;
    AudioSource audioSource;
    int currentSceneIndex;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        PlayMusic();
    }

    public void StopMenuMusic(){
        audioSource.Stop();
        StartCoroutine(MusicPlayDelay(musicDelay));
    }

    private void PlayMusic(){
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(currentSceneIndex == 0){
            audioSource.clip = musicClips[1];
            audioSource.Play();
        }
    }

    private IEnumerator MusicPlayDelay(float delay){
        yield return new WaitForSeconds(delay);
        audioSource.clip = musicClips[0];
        audioSource.Play();
    }
}
