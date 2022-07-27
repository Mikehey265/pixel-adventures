using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    #region Private Variables

    [SerializeField] float spinSpeed;
    [SerializeField] float throwDistance;
    [SerializeField] float throwSpeed;
    [SerializeField] int boomerangDamage;
    [SerializeField] float thrust;
    [SerializeField] bool goForward;
    PlayerController playerController;
    Vector2 throwDirection;
        
    #endregion

    #region Unity Methods

    private void Awake() {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start() {
        FindPositionToThrow();
    }

    private void Update() {
        transform.Rotate(new Vector3(0f, 0f, spinSpeed) * Time.deltaTime);

        DetectDestination();
        MoveBoomerang();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        goForward = false;
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();

        if(enemy){
            enemy.TakeDamage(boomerangDamage);
            other.gameObject.GetComponent<Knockback>().GetKnockedBack(transform, thrust);
        }
    }
        
    #endregion

    #region Private Methods

    private void FindPositionToThrow(){
        Animator playerAnimator = playerController.GetComponent<Animator>();

        if(playerAnimator.GetFloat("lastMoveX") == 1){
            throwDirection = new Vector2(playerController.transform.position.x + throwDistance, playerController.transform.position.y);
        }
        else if(playerAnimator.GetFloat("lastMoveX") == -1){
            throwDirection = new Vector2(playerController.transform.position.x - throwDistance, playerController.transform.position.y);
        }
        else if(playerAnimator.GetFloat("lastMoveY") == 1){
            throwDirection = new Vector2(playerController.transform.position.x, playerController.transform.position.y + throwDistance);
        }
        else if(playerAnimator.GetFloat("lastMoveY") == -1){
            throwDirection = new Vector2(playerController.transform.position.x, playerController.transform.position.y - throwDistance);
        }

        goForward = true;
    }

    private void DetectDestination(){
        if(goForward && Vector2.Distance(throwDirection, transform.position) < 0.5f){
            goForward = false;
        }
    }

    private void MoveBoomerang(){
        if(goForward){
            transform.position = Vector2.MoveTowards(transform.position, throwDirection, throwSpeed * Time.deltaTime);
        }
        else if(!goForward){
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerController.transform.position.x, playerController.transform.position.y), throwSpeed * Time.deltaTime);
        }

        if(!goForward && Vector2.Distance(playerController.transform.position, transform.position) < 1f){
            playerController.itemInUse = false;
            Destroy(this.gameObject);
        }
    }
        
    #endregion
}
