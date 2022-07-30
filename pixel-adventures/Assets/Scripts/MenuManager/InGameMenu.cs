using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : Singleton<InGameMenu>
{
    #region Private Variables

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject[] uiElements;
    PlayerControls playerControls;
    List<GameObject> uiEnabledBeforePause = new List<GameObject>();
    bool gamePaused;

    #endregion

    #region Unity methods

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void Start() {
        playerControls.Menu.Use.performed += _ => PauseMenuActions();
        gamePaused = false;
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        if(playerControls != null){
            playerControls.Disable();
        }
    }

    #endregion

    #region Public Methods

    public void ResumeGame(){
        Time.timeScale = 1;
        PlayerController.Instance.canMove = true;
        PlayerController.Instance.canAttack = true;
        gamePaused = false;
        pauseMenu.SetActive(false);

        foreach(GameObject ui in uiEnabledBeforePause){
            ui.SetActive(true);
        }

        uiEnabledBeforePause.Clear();
    }

    #endregion

    #region Private Methods

    private void PauseMenuActions(){
        if(!gamePaused){
            Time.timeScale = 0;
            PlayerController.Instance.canMove = false;
            PlayerController.Instance.canAttack = false;
            gamePaused = true;
            pauseMenu.SetActive(true);

            for(int i = 0; i < uiElements.Length; i++){
                if(uiElements[i].activeInHierarchy == true){
                    uiElements[i].SetActive(false);
                    uiEnabledBeforePause.Add(uiElements[i]);
                    // Debug.Log(uiElements[i].ToString());
                    // Debug.Log(uiEnabledBeforePause.Count);
                }
            }
        }else{
            ResumeGame();
        }
    }

    #endregion
}
