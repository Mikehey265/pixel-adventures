using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPrefs : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] bool canUse;
    [SerializeField] MenuController menuController;

    [Header("Volume Settings")]
    [SerializeField] TMP_Text volumeTextValue;
    [SerializeField] Slider volumeSlider;

    [Header("Brightness Settings")]
    [SerializeField] Slider brightnessSlider;
    [SerializeField] TMP_Text brightnessTextValue;

    [Header("Quality Settings")]
    [SerializeField] TMP_Dropdown qualityDropdown;

    [Header("Fullscreen Settings")]
    [SerializeField] Toggle fullScreenToggle;

    [Header("Sensitivity Settings")]
    [SerializeField] TMP_Text controllerSenTextValue;
    [SerializeField] Slider controllerSenSlider;

    [Header("Invert Y Settings")]
    [SerializeField] Toggle invertYToggle;

    private void Awake() {
        if(canUse){
            if(PlayerPrefs.HasKey(menuController.volumePrefs)){
                float localVolume = PlayerPrefs.GetFloat(menuController.volumePrefs);

                volumeTextValue.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else{
                menuController.ResetButton("Audio");
            }

            if(PlayerPrefs.HasKey(menuController.qualityPrefs)){
                int localQuality = PlayerPrefs.GetInt(menuController.qualityPrefs);

                qualityDropdown.value = localQuality;
                QualitySettings.SetQualityLevel(localQuality);
            }

            if(PlayerPrefs.HasKey(menuController.fullScreenPrefs)){
                int localFullscreen = PlayerPrefs.GetInt(menuController.fullScreenPrefs);

                if(localFullscreen == 1){
                    Screen.fullScreen = true;
                    fullScreenToggle.isOn = true;
                }else{
                    Screen.fullScreen = false;
                    fullScreenToggle.isOn = false;
                }
            }

            if(PlayerPrefs.HasKey(menuController.brigthnessPrefs)){
                float localBrightness = PlayerPrefs.GetFloat(menuController.brigthnessPrefs);

                brightnessSlider.value = localBrightness;
                brightnessTextValue.text = localBrightness.ToString("0.0");
                //Change the brightness
            }

            if(PlayerPrefs.HasKey(menuController.sensitivityPrefs)){
                float localSensitivity = PlayerPrefs.GetFloat(menuController.sensitivityPrefs);

                controllerSenTextValue.text = localSensitivity.ToString("0.0");
                controllerSenSlider.value = localSensitivity;
                menuController.mainControllerSensitivity = Mathf.RoundToInt(localSensitivity);
            }

            if(PlayerPrefs.HasKey(menuController.invertYPrefs)){
                if(PlayerPrefs.GetInt(menuController.invertYPrefs) == 1){
                    invertYToggle.isOn = true;
                }else{
                    invertYToggle.isOn = false;
                }
            }
        }
    }
}
