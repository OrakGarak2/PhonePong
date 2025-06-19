using System;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [Header("오디오")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(OnMasterSliderValueChanged);
        musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderValueChanged);
    }

    private void Start()
    {
        masterSlider.value = AudioManager.Instance.masterVolume;
        musicSlider.value = AudioManager.Instance.musicVolume;
        sfxSlider.value = AudioManager.Instance.sfxVolume;
    }

    private void OnMasterSliderValueChanged(float value)
    {
        AudioManager.Instance.masterVolume = value;
        masterSlider.value = AudioManager.Instance.masterVolume;
    }

    private void OnMusicSliderValueChanged(float value)
    {
        AudioManager.Instance.musicVolume = value;
        musicSlider.value = AudioManager.Instance.musicVolume;
    }

    private void OnSFXSliderValueChanged(float value)
    {
        AudioManager.Instance.sfxVolume = value;
        sfxSlider.value = AudioManager.Instance.sfxVolume;
    }
}
