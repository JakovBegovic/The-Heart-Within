using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider audioLevelSlider;
    public AudioMixer audioMixer;

    private float audioLevel;
    private void Start()
    {
        audioMixer.GetFloat("volumeExposedParam", out audioLevel);
        audioLevelSlider.value = audioLevel;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volumeExposedParam", volume);
        Debug.Log(volume);
    }
}
