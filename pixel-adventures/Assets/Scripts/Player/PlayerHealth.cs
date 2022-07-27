using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    #region Public Variables

    public int currentHealth;
    public int maxHealth;
        
    #endregion

    #region Private Variables

    [SerializeField] int startinghealth;
    [SerializeField] Animator animator;
    [SerializeField] Material whiteFlashMaterial;
    [SerializeField] float whiteFlashTime;
    [SerializeField] float damageRecoveryTime;
    [SerializeField] float respawnTime;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Material defaultMaterial;
    bool canTakeDamage;
    bool isDead;
        
    #endregion

    #region Unity Methods

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = startinghealth;
        maxHealth = startinghealth;
        defaultMaterial = spriteRenderer.material;
    }

    private void Start() {
        canTakeDamage = true;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy") && canTakeDamage && currentHealth > 0){
            EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();
            TakeDamage(enemy.damageDone);
            GetComponent<Knockback>().GetKnockedBack(other.gameObject.transform, enemy.enemyKnockbackThrust);
        }
    }
        
    #endregion

    #region Public Methods

    public void CheckIfDead(){
        if(currentHealth <= 0 && !isDead){
            isDead = true;
            PlayerController.Instance.canMove = false;
            PlayerController.Instance.canAttack = false;
            animator.SetTrigger("dead");
            StartCoroutine(RespawnRoutine());
        }else{
            PlayerController.Instance.canMove = true;
            PlayerController.Instance.canAttack = true;
        }
    }

    public void TakeDamage(int damage){
        spriteRenderer.material = whiteFlashMaterial;
        currentHealth -= damage;
        canTakeDamage = false;
        StartCoroutine(WhiteFlashRoutine());
        StartCoroutine(DamageRecoveryTimeRoutine());
    }
        
    #endregion

    #region Private Methods

    private IEnumerator WhiteFlashRoutine(){
        yield return new WaitForSeconds(whiteFlashTime);
        spriteRenderer.material = defaultMaterial;
    }

    private IEnumerator DamageRecoveryTimeRoutine(){
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private IEnumerator RespawnRoutine(){
        yield return new WaitForSeconds(respawnTime);
        Destroy(PlayerController.Instance.gameObject);
        SceneManager.LoadScene(0);
    }
        
    #endregion
}
