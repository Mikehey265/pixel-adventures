using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaloonNotification : MonoBehaviour
{
    #region Private Variables

    [SerializeField] Animator baloonAnimator;
    [SerializeField] GameObject baloonPosition;
    bool questGiven;
    bool inRange;
    bool showBaloon;
    const string player = "Player";

    #endregion

    #region Unity Methods

    private void Update() {
        ToggleBaloonIcon();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == player){
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == player){
            inRange = false;
        }
    }

    #endregion

    #region Private Variables

    private void ToggleBaloonIcon(){
        if(inRange){
            if(!DialogueActivator.FindObjectOfType<DialogueActivator>().canActivate){
                baloonPosition.SetActive(true);
                baloonAnimator.SetTrigger("playAnim");
            }else{
                baloonPosition.SetActive(false);
            }

            if(DialogueManager.Instance.justStarted){
                questGiven = true;
            }

            if(questGiven){
                baloonPosition.SetActive(false);
            }
        }else{
            baloonPosition.SetActive(false);
        }
    }

    #endregion


}
