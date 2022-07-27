using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Public Variables
    public bool canMove;
    public float enemyKnockbackThrust;
    public int damageDone;

    #endregion

    #region Private Variables
        
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] SpriteRenderer spriteRenderer;

    Vector2 movement;

    #endregion

    private void Start() {
        StartCoroutine(RandomMovement());
        canMove = true;
    }

    private void Update() {
        Move();
    }

    private void Move(){
        if(!canMove){return;}

        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        if(movement.x == -1){
            spriteRenderer.flipX = true;
        }else{
            spriteRenderer.flipX = false;
        }
    }

    private IEnumerator RandomMovement(){
        while(true){
            float rand = Random.Range(-5, 5);
            movement.x = Random.Range(-1, 2);
            movement.y = Random.Range(-1, 2);
            yield return new WaitForSeconds(rand);
        }
    }
}
