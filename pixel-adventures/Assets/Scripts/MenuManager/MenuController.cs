using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    #region String Variables

    public string volumePrefs = "masterVolume";
    public string invertYPrefs = "masterInvertY";
    public string sensitivityPrefs = "masterSensitivity";
    public string brigthnessPrefs = "masterBrightness";
    public string qualityPrefs = "masterQuality";
    public string fullScreenPrefs = "masterFullscreen";


    #endregion

    [Header("Volume Settings")]
    [SerializeField] TMP_Text musicTextValue;
    [SerializeField] Slider musicSlider;
    [SerializeField] float defaultVolume;
    // [SerializeField] TMP_Text sfxTextValue;
    // [SerializeField] Slider sfxSlider;

    [Header("Gameplay Settings")]
    [SerializeField] TMP_Text controllerSenTextValue;
    [SerializeField] Slider controllerSenSlider;
    [SerializeField] int defaultSensitivity;
    public int mainControllerSensitivity;

    [Header("Invert Settings")]
    [SerializeField] Toggle invertYToggle;

    [Header("Graphics Settings")]
    [SerializeField] Slider brightnessSlider;
    [SerializeField] TMP_Text brightnessTextValue;
    [SerializeField] float defaultBrightness;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    int qualityLevel;
    bool isFullScreen;
    float brightnessLevel;

    [Space(10)]
    [SerializeField] TMP_Dropdown qualityDropdown;
    [SerializeField] Toggle fullScreenToggle;

    [Header("Confirmation")]
    [SerializeField] GameObject confirmationPrompt;

    [Header("Levels To Load")]
    [SerializeField] GameObject noSaveGamePanel;
    [SerializeField] int mainMenuIndex;
    public string newGameLevel;
    private string levelToLoad;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject backgroundImage;

    private void Start() {
        Resolution();
    }

    public void StartNewGame(){
        ButtonSound();
        SceneManager.LoadScene(newGameLevel);
    }

    public void LoadGame(){
        ButtonSound();
        if(PlayerPrefs.HasKey("SavedLevel")){
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }else{
            noSaveGamePanel.SetActive(true);
        }
    }

    public void ReturnToMainMenu(){
        ButtonSound();
        SceneManager.LoadScene(mainMenuIndex);
    }

    public void ExitGame(){
        ButtonSound();
        Application.Quit();
    }

    #region Audio Methods

    public void SetMusicVolume(float volume){
        AudioListener.volume = volume;
        musicTextValue.text = volume.ToString("0.0");
    }

    public void ApplyMusicVolume(){
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        ButtonSound();
        StartCoroutine(ConfirmationPrompt());
    }

    #endregion

    #region Gameplay Methods

    public void SetControllerSensitivity(float sensitivity){
        mainControllerSensitivity = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply(){
        if(invertYToggle.isOn){
            PlayerPrefs.SetInt("masterInvertY", 1);
        }else{
            PlayerPrefs.SetInt("masterInvertY", 0);
        }

        ButtonSound();
        PlayerPrefs.SetFloat("masterSensitivity", mainControllerSensitivity);
        StartCoroutine(ConfirmationPrompt());
    }

    #endregion

    #region Graphics Methods

    public void SetBrightness(float brightness){
        brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool _isFullScreen){
        isFullScreen = _isFullScreen;
    }

    public void SetQuality(int qualityIndex){
        qualityLevel = qualityIndex;
    }

    public void GraphicsApply(){
        PlayerPrefs.SetFloat("masterBrightness", brightnessLevel);

        PlayerPrefs.SetInt("masterQuality", qualityLevel);
        QualitySettings.SetQualityLevel(qualityLevel);

        PlayerPrefs.SetInt("masterFullscreen", (isFullScreen ? 1 : 0));
        Screen.fullScreen = isFullScreen;

        //resolution

        ButtonSound();
        StartCoroutine(ConfirmationPrompt());
    }

    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void Resolution(){
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height){
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    #endregion

    public void ResetButton(string MenuType){
        if(MenuType == "Audio"){
            AudioListener.volume = defaultVolume;
            musicSlider.value = defaultVolume;
            musicTextValue.text = defaultVolume.ToString("0.0");
            ButtonSound();
            ApplyMusicVolume();
        }

        if(MenuType == "Gameplay"){
            controllerSenTextValue.text = defaultSensitivity.ToString("0");
            controllerSenSlider.value = defaultSensitivity;
            mainControllerSensitivity = defaultSensitivity;

            invertYToggle.isOn = false;
            ButtonSound();
            GameplayApply();
        }

        if(MenuType == "Graphics"){
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;

            ButtonSound();
            GraphicsApply();
        }
    }

    public void ButtonSound(){
        AudioManager.Instance.PlayMenuSFX(AudioManager.Instance.menuSFX[0]);
    }

    public IEnumerator ConfirmationPrompt(){
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(1);
        confirmationPrompt.SetActive(false);
    }
}
