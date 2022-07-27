using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public bool isBombExplosion;

    #region Private Variables

    [SerializeField] int damageAmount;
    [SerializeField] float knockbackTime;
    [SerializeField] float knockbackThrust;
    const string enemy = "Enemy";
    const string player = "Player";
        
    #endregion

    #region Unity Methods

    private void OnTriggerEnter2D(Collider2D other) {
        DamageToEnemy(other);
        DamageToPlayer(other);
        DestroyEnvironment(other);
        DestroyBoulder(other);
    }
        
    #endregion

    #region Private Methods

    private void DamageToEnemy(Collider2D other){
        if(other.gameObject.CompareTag(enemy)){
            other.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
            other.GetComponent<Knockback>().GetKnockedBack(PlayerController.Instance.transform, knockbackThrust);
        }
    }

    private void DamageToPlayer(Collider2D other){
        if(other.gameObject.CompareTag(player)){
            other.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            other.GetComponent<Knockback>().GetKnockedBack(transform, knockbackThrust);
        }
    }

    private void DestroyEnvironment(Collider2D other){
        if(other.GetComponent<Breakable>()){
            other.GetComponent<Breakable>().BreakObject();
        }
    }

    private void DestroyBoulder(Collider2D other){
        if(other.GetComponent<Boulder>() && isBombExplosion){
            other.GetComponent<Boulder>().DestroyBoulder();
        }
    }
        
    #endregion
}
