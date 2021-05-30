using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerSettings : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    public GameObject settingsMenu;

    private void Start()
    {
        musicSlider.value = GameManager.instance.musicSliderValue;
        sfxSlider.value = GameManager.instance.sfxSliderValue;

        mainMixer.SetFloat("MusicVolume", Mathf.Log10(GameManager.instance.musicSliderValue) * 20);
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(GameManager.instance.sfxSliderValue) * 20);

        settingsMenu.SetActive(false);
    }

    public void MusicChange(float sliderValue)
    {
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);

        GameManager.instance.musicSliderValue = sliderValue;
    }

    public void SfxChange(float sliderValue)
    {
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);

        GameManager.instance.sfxSliderValue = sliderValue;
    }
}
