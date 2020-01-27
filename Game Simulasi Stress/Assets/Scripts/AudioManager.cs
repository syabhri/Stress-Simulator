using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    [Tooltip("Note : the filename must match the Exposed variable in audio mixer")]
    public FloatVariable[] volumes;

    private void Start()
    {
        foreach (FloatVariable volume in volumes)
        {
            float value = PlayerPrefs.GetFloat(volume.name, 0);
            audioMixer.SetFloat(volume.name, Mathf.Log10(value) * 20);
            volume.SetValue(value);
            volume.OnValueChange += ChangeVolume;
        }
    }

    private void OnDestroy()
    {
        foreach (FloatVariable volume in volumes)
        {
            volume.OnValueChange -= ChangeVolume;
        }
    }

    public void ChangeVolume(FloatVariable volume)
    {
        audioMixer.SetFloat(volume.name, Mathf.Log10(volume.value) *20);
        PlayerPrefs.SetFloat(volume.name, volume.value);
    }


}
