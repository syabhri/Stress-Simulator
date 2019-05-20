using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables and Class Reverences
    public static GameManager instance;

    public Sound[] sounds;
    public Stats playerStats;
    #endregion

    #region Unity Event Function
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        #region GameManager Awake
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        #endregion
        #region SoundManager Awake
        //create audioSource to current object for each audio clip
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.mute = s.mute;
        }
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    #endregion

    #region GameManager function
    public void PlayGame()
    {
        ChangeScene("GameplayScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public static void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public static void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }
    #endregion

    #region SoundManager funtion
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not Found!");
            return;
        }
        s.source.Play();       
    }
    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not Found!");
            return;
        }
        s.source.Stop();
    }
    public void mute()
    {
        foreach (var sound in sounds)
        {
            if (sound.source.mute)
            {
                sound.source.mute = false;
            }
            else
            {
                sound.source.mute = true;
            }
        }
    }
    public void changeVolume(float volume)
    {
        foreach (var sound in sounds)
        {
            sound.source.volume = volume;
        }
    }
    #endregion
}
