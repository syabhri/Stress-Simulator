using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioControlSlider : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;
    public string exposedParameter;

    private void Start()
    {
        audioMixer.GetFloat(exposedParameter, out float value);
        slider.value = value;
    }

    public void UpdateChanges()
    {
        audioMixer.SetFloat(exposedParameter, slider.value);
        PlayerPrefs.SetFloat(exposedParameter, slider.value);
    }
}
