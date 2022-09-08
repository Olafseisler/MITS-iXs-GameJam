using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    void Start()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
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

}
