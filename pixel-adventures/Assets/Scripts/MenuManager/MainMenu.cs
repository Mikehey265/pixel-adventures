using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Private Variables
    [SerializeField] int sceneToStartGame;
    [SerializeField] float loadSceneDelay;
    [SerializeField] GameObject aboutPanel;
    [SerializeField] GameObject blackOutPanel;

    #endregion

    #region Public Methods

    public void PlayGame(){
        AudioManager.Instance.PlayMenuSFX(AudioManager.Instance.menuSFX[0]);
        StartCoroutine(LoadSceneDelay(loadSceneDelay));
    }

    public void AboutOn(){
        AudioManager.Instance.PlayMenuSFX(AudioManager.Instance.menuSFX[0]);
        aboutPanel.SetActive(true);
        blackOutPanel.SetActive(true);
    }

    public void AboutOff(){
        AudioManager.Instance.PlayMenuSFX(AudioManager.Instance.menuSFX[0]);
        aboutPanel.SetActive(false);
        blackOutPanel.SetActive(false);
    }

    public void ExitGame(){
        AudioManager.Instance.PlayMenuSFX(AudioManager.Instance.menuSFX[0]);
        Application.Quit();
    }

    #endregion

    #region Private Methods

    private IEnumerator LoadSceneDelay(float delay){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToStartGame);
    }

    #endregion
}
