using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionMenu : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public Toggle FullscreenToggle;
    public Toggle VSyncToggle;
    Resolution[] resolutions;
    [SerializeField] GameObject resolutionOption;

    void Start()
    {
        if (BuildConstants.isWebGL)
        {
            resolutionOption.SetActive(false);
        }
        else
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            List<string> options = new();
            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height + " @ " + Mathf.Round((float)resolutions[i].refreshRateRatio.value) + "hz";
                options.Add(option);
                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSyncPreference");
        if (Screen.fullScreen)
        {
            FullscreenToggle.isOn = true;
        }
        VSyncToggle.SetIsOnWithoutNotify(Convert.ToBoolean(PlayerPrefs.GetInt("vSyncPreference")));

    }
    public void updateMusicVolume()
    {
        if (AudioManager.instance != null)
        {
            PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
            AudioManager.instance.musicVolumeChanged();
        }
    }

    public void updateEffectsVolume()
    {
        if (AudioManager.instance != null)
        {
            PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeSlider.value);
            AudioManager.instance.effectVolumeChanged();
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetvSync()
    {
        QualitySettings.vSyncCount = Convert.ToInt32(VSyncToggle.isOn);
        PlayerPrefs.SetInt("vSyncPreference",
           QualitySettings.vSyncCount);
        Debug.Log("VSync toggled: " + VSyncToggle.isOn);
    }
}
