using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string transitionName;

    [SerializeField] float moveSpeedWaitTime;

    private void Start() {
        if(PlayerController.Instance != null){
            if(transitionName == PlayerController.Instance.areaTransitionName){
                PlayerController.Instance.transform.position = transform.position;
                StartCoroutine(PlayerMoveDelayRoutine());

                if(UIFade.Instance != null){
                    UIFade.Instance.FadeToClear();
                }
            }
        }
    }

    private IEnumerator PlayerMoveDelayRoutine(){
        PlayerController.Instance.canMove = false;
        yield return new WaitForSeconds(moveSpeedWaitTime);
        PlayerController.Instance.canMove = true;
    }
}
