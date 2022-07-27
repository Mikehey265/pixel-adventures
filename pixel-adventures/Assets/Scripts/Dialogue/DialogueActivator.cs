using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    #region Public Variables

    [SerializeField] public Sprite portrait;
    public string[] lines;
    public bool isPerson;
        
    #endregion

    #region Private Variables

    [SerializeField] GameObject buttonUI;
    PlayerControls playerControls;
    bool canActivate;
    const string player = "Player";
        
    #endregion

    #region Unity Methods

    private void Awake() {
        playerControls = new PlayerControls();
    }

    private void Start() {
        playerControls.Interact.Use.performed += _ => OpenDialogue();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable(); 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == player){
            buttonUI.SetActive(true);
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == player){
            buttonUI.SetActive(false);
            canActivate = false;
        }
    }
        
    #endregion

    #region Private Methods

    private void OpenDialogue(){
        if(canActivate){
            if(!DialogueManager.Instance.dialogueBox.activeInHierarchy){
                DialogueManager.Instance.ShowDialogue(lines, isPerson);
                DialogueManager.Instance.ShowPortrait(isPerson);
                PlayerController.Instance.canAttack = false;
                PlayerController.Instance.DialogueStopMove();
            }else{
                DialogueManager.Instance.ContinueDialogue();
            }
        }
    }
        
    #endregion
}
