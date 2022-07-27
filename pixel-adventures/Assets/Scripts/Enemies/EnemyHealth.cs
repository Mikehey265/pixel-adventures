using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    #region Private Variables

    [SerializeField] int startinghealth;
    [SerializeField] int currentHealth;
    [SerializeField] Material whiteFlashMaterial;
    [SerializeField] float whiteFlashTime;
    [SerializeField] GameObject deathVFX;
    SpriteRenderer spriteRenderer;
    Material defaultMaterial;
        
    #endregion

    #region Unity Methods

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
    }

    private void Start() {
        currentHealth = startinghealth;
    }

    private void Update() {
        DetectDeath();
    }
        
    #endregion

    #region Public Methods

    public void TakeDamage(int damage){
        currentHealth -= damage;
        AudioManager.Instance.PlayEnemySFX(AudioManager.Instance.enemySFX[0]);
        spriteRenderer.material = whiteFlashMaterial;
        StartCoroutine(WhiteFlashRoutine(whiteFlashTime));
    }
        
    #endregion

    #region Private Methods

    private void DetectDeath(){
        if(currentHealth <= 0){
            AudioManager.Instance.PlayEnemySFX(AudioManager.Instance.enemySFX[1]);
            Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private IEnumerator WhiteFlashRoutine(float time){
        yield return new WaitForSeconds(time);
        spriteRenderer.material = defaultMaterial;
    }
        
    #endregion
}
