using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : Singleton<InGameMenu>
{
    #region Private Variables

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject[] uiElements;
    PlayerControls playerControls;
    List<GameObject> uiEnabledBeforePause = new List<GameObject>();
    bool gamePaused;
    bool canClickEsc;

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
        if(gamePaused && pauseMenu.activeInHierarchy == false) return;
        Time.timeScale = 1;
        PlayerController.Instance.canMove = true;
        PlayerController.Instance.canAttack = true;
        PlayerController.Instance.ToggleGameState();
        AudioManager.Instance.PlayMenuSFX(AudioManager.Instance.menuSFX[0]);
        gamePaused = false;
        pauseMenu.SetActive(false);

        foreach(GameObject ui in uiEnabledBeforePause){
            ui.SetActive(true);
        }

        uiEnabledBeforePause.Clear();
    }

    public void MainMenu(){
        AudioManager.Instance.PlayMenuSFX(AudioManager.Instance.menuSFX[0]);
        SceneManager.LoadScene(0);
        Destroy(Singleton<PlayerController>.Instance.gameObject);
        Destroy(Singleton<AudioManager>.Instance.gameObject);
        Destroy(Singleton<InventoryManager>.Instance.gameObject);
        Destroy(Singleton<CameraController>.Instance.gameObject);
    }

    #endregion

    #region Private Methods

    private void PauseMenuActions(){
        if(!gamePaused){
            Time.timeScale = 0;
            PlayerController.Instance.canMove = false;
            PlayerController.Instance.canAttack = false;
            PlayerController.Instance.ToggleGameState();
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
