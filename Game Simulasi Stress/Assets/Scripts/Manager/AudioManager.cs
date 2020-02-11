using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    [Tooltip("Note : the filename must match the Exposed variable in audio mixer")]
    public FloatContainer[] volumes;

    private void Start()
    {
        foreach (FloatContainer volume in volumes)
        {
            float value = PlayerPrefs.GetFloat(volume.name, 0);
            audioMixer.SetFloat(volume.name, Mathf.Log10(value) * 20);
            volume.Value = value;

            volume.OnValueChanged += ChangeVolume;
        }
    }

    private void OnDestroy()
    {
        foreach (FloatContainer volume in volumes)
        {
            volume.OnValueChanged -= ChangeVolume;
        }
    }

    public void ChangeVolume(FloatContainer volume)
    {
        audioMixer.SetFloat(volume.name, Mathf.Log10(volume.Value) *20);
        PlayerPrefs.SetFloat(volume.name, volume.Value);
    }
}
