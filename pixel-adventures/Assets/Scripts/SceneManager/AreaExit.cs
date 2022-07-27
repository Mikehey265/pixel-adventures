using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    #region Public Variables

    public AreaEntrance entrance;
    public float waitToLoad;
        
    #endregion

    #region Private Variables

    [SerializeField] string areaToLoad;
    [SerializeField] string areaTransitionName;
    bool shouldLoadAfterFade;
        
    #endregion

    #region Unity Methods

    private void Start() {
        entrance.transitionName = areaTransitionName;
    }

    private void Update() {
        if(shouldLoadAfterFade){
            waitToLoad -= Time.deltaTime;
            if(waitToLoad <= 0){
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            shouldLoadAfterFade = true;
            UIFade.Instance.FadeToBlack();
            PlayerController.Instance.areaTransitionName = areaTransitionName;
        }
    }
        
    #endregion
}
