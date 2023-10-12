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
        FullscreenToggle.isOn = Screen.fullScreen;
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
        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = Convert.ToBoolean(FullscreenToggle.isOn);
    }
}
