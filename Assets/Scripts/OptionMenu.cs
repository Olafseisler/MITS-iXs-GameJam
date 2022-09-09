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

    void Start()
    {
        resolutions = Screen.resolutions;
        List<string> options = new();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " +
                     resolutions[i].height;
            options.Add(option);
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSyncPreference");
        FullscreenToggle.SetIsOnWithoutNotify(Screen.fullScreen);
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

    public void SetResolution()
    {
        Debug.Log("Resolution set to index " + resolutionDropdown.value);
        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width,
                  resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionPreference",
               resolutionDropdown.value);
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = FullscreenToggle.isOn;
        PlayerPrefs.SetInt("FullscreenPreference",
           Convert.ToInt32(FullscreenToggle.isOn));
        Debug.Log("Fullscreen toggled: " + FullscreenToggle.isOn);
    }

    public void SetvSync()
    {
        QualitySettings.vSyncCount = Convert.ToInt32(VSyncToggle.isOn);
        PlayerPrefs.SetInt("vSyncPreference",
           QualitySettings.vSyncCount);
        Debug.Log("VSync toggled: " + VSyncToggle.isOn);
    }
}
