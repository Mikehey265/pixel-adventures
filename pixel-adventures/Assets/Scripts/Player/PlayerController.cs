using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    #region Public Variables
        
    public string areaTransitionName;
    public bool canMove;
    public bool canAttack;
    public bool itemInUse;
    public GameObject itemEquipped;
    [SerializeField] public Sprite portrait;

    #endregion

    #region Private Variables

    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] Animator animator;

    [SerializeField] GameObject hitBox_Top;
    [SerializeField] GameObject hitBox_Bottom;
    [SerializeField] GameObject hitBox_Left;
    [SerializeField] GameObject hitBox_Right;

    [SerializeField] float attackDelay;

    PlayerControls playerControls;
    enum GameState {Playing, Paused};
    GameState currentGameState;
    Vector2 movement;

    #endregion

    #region Unity Methods

    protected override void Awake() {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void Start(){
        playerControls.Movement.Run.performed += _ => StartRun();
        playerControls.Movement.Run.canceled += _ => StopRun();
        playerControls.Combat.Attack.performed += _ => Attack();
        playerControls.Item.Use.performed += _ => UseItem();

        canMove = true;
        canAttack = true;
        currentGameState = GameState.Playing;
    }

    private void OnEnable(){
        playerControls.Enable();
    }

    private void OnDisable(){
        if(playerControls != null){
            playerControls.Disable();
        }
    }

    private void Update(){
        PlayerInput();
        itemEquipped = InventoryManager.Instance.itemEquippedInventory;
    }

    private void FixedUpdate() {
        Move();
    }

    #endregion

    #region Public Methods

    public void SpawnItem(){
        itemEquipped = InventoryManager.Instance.itemEquippedInventory;

        if(itemInUse || !itemEquipped){return;}

        itemInUse = true;
        ItemDisplay itemDisplay = InventoryManager.Instance.currentSelectedItem.GetComponent<ItemDisplay>();

        if(itemDisplay){
            if(itemDisplay.item.itemType == "Bomb"){
                itemInUse = false;
            }

            if(animator.GetFloat("lastMoveX") == 1){
                Instantiate(itemEquipped, new Vector2(transform.position.x + 1, transform.position.y + 0.5f), transform.rotation);
            }
            else if (animator.GetFloat("lastMoveX") == -1) {
                Instantiate(itemEquipped, new Vector2(transform.position.x - 1, transform.position.y + 0.5f), transform.rotation);
            }
            else if (animator.GetFloat("lastMoveY") == 1) {
                Instantiate(itemEquipped, new Vector2(transform.position.x, transform.position.y + 1.5f), transform.rotation);
            }
            else if (animator.GetFloat("lastMoveY") == -1) {
                Instantiate(itemEquipped, new Vector2(transform.position.x, transform.position.y - 1f), transform.rotation);
            }
        }
    }
    
    public void ActivateCollider(){
        if(animator.GetFloat("lastMoveY") == 1){
            hitBox_Top.SetActive(true);
        }
        if(animator.GetFloat("lastMoveY") == -1){
            hitBox_Bottom.SetActive(true);
        }
        if(animator.GetFloat("lastMoveX") == -1){
            hitBox_Left.SetActive(true);
        }
        if(animator.GetFloat("lastMoveX") == 1){
            hitBox_Right.SetActive(true);
        }
    }

    public void canMoveFunction(){
        canMove = true;

        hitBox_Top.SetActive(false);
        hitBox_Bottom.SetActive(false);
        hitBox_Left.SetActive(false);
        hitBox_Right.SetActive(false);
    }

    public void ToggleGameState(){
        if(currentGameState == GameState.Paused){
            currentGameState = GameState.Playing;
        }else{
            animator.SetFloat("moveX", 0f);
            animator.SetFloat("moveY", 0f);
            currentGameState = GameState.Paused;
        }
    }

    public void PauseGame(){
        currentGameState = GameState.Paused;
        Time.timeScale = 0f;
    }

    public void ResumeGame(){
        currentGameState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void DialogueStopMove(){
        animator.SetFloat("moveX", 0f);
        animator.SetFloat("moveY", 0f);
    }

    #endregion

    #region Private Methods

    private void PlayerInput(){
        if(!canMove || currentGameState == GameState.Paused){return;}

        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);

        if(movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1){
            if(canMove){
                animator.SetFloat("lastMoveX", movement.x);
                animator.SetFloat("lastMoveY", movement.y);
            }
        }
    }

    private void Move(){
        if(!canMove){return;}

        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private void StartRun(){
        animator.SetBool("isRunning", true);
        moveSpeed += runSpeed;
    }

    private void StopRun(){
        animator.SetBool("isRunning", false);
        moveSpeed -= runSpeed;
    }

    private void Attack(){
        if(canAttack && currentGameState != GameState.Paused){
            rb.velocity = Vector2.zero;
            canMove = false;
            StartCoroutine(AttackDelay(attackDelay));
        }
    }

    private void UseItem(){
        if(canAttack && currentGameState != GameState.Paused && (itemEquipped != null || itemInUse == true)){
            rb.velocity = Vector2.zero;
            canMove = false;
            animator.SetTrigger("useItem");
        }
    }

    private IEnumerator AttackDelay(float delay){
        animator.SetTrigger("attack");
        AudioManager.Instance.PlayAttackSFX();
        canAttack = false;
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }

    #endregion
}
