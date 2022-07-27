using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] float knockbackTime;
    Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockbackThrust){
        Vector2 difference = transform.position - damageSource.position;
        difference = difference.normalized * knockbackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);

        if(GetComponent<PlayerController>()){
            PlayerController.Instance.canMove = false;
        }

        StartCoroutine(KnockbackRoutine());
    }

    private IEnumerator KnockbackRoutine(){
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;

        if (GetComponent<PlayerController>()) {
            PlayerController.Instance.canMove = true;
            GetComponent<PlayerHealth>().CheckIfDead();
        }
    }
}
