// ----------------------------------------------------------------------------
// Introduction to AUDIO in Unity
// link : https://www.youtube.com/watch?v=6OT43pvUyfY&t=214s
// 
// Author : Brackeys
// Date :   01/06/17
//
// Comments : Syabhri
// Date :     6/25/19
// ----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    // variabel ini digunakan untuk mengakses object ini dari berbagai scene
    // sehingga tidak terpengaruh oleh pergantian scene
    public static AudioManager instance;

    // array sound berisi sound/music yang di isi melalui inspector
    public Sound[] sounds;

    // awake di panggil sebelum start pada awal script dijalankan
    void Awake()
    {
        // jika variable instace belum ada maka massukkan referensi object audio manager
        // saat ini kedalam variable instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // atau jika instace sudah ada maka hancurkan object ini
            // sehingga object lama akan tetap digunakan
            Destroy(gameObject);
            return;
        }

        // jangan hancurkan object ketika berpindah scene
        DontDestroyOnLoad(gameObject);

        // membuat componen audiosource untuk setiap sound yang ada di array sounds
        foreach (Sound s in sounds)
        {
            // memebuat component audiosource dan memasukkan referensi ke variable array sound s.source
            // variable ini yang diakses ketika ingin mengubah properti sound seperti volume
            s.source = gameObject.AddComponent<AudioSource>();
            // memasukkan clip sound ke audiosource
            s.source.clip = s.clip;
            
            // mengatur properti audiosource sesuai dengan yang diteapkan di array
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.mute = s.mute;
        }
    }

    // memainkan sound sesuai nama pada array sounds
    public void PlaySound(string name)
    {
        // mencari sound di array yang sesuai parameter name
        // kemudian memasukkan referensi sound yang ditemukan ke variable s
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            // jika tidak ditemukan maka tampilkan warning di console
            Debug.LogWarning("Sound: " + name + " not Found!");
            return;
        }
        // mainkan sound yang telah ditemukan di array melalui variable source
        s.source.Play();
    }

    // menghentikan sound/music sesuai nama pada array sounds
    public void StopSound(string name)
    {
        // mencari sound/music di array yang sesuai parameter name
        // kemudian memasukkan referensi sound yang ditemukan ke variable s
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            // jika tidak ditemukan maka tampilkan warning di console
            Debug.LogWarning("Sound: " + name + " not Found!");
            return;
        }
        // hentikan sund yang telah ditemukan
        s.source.Stop();
    }

    // membisukan(mute) dan menghilangkan bisu(unmute) semua sound yang ada di array sounds
    public void mute()
    {
        foreach (var sound in sounds)
        {
            // membisukan(mute) semua sound yang sedang di putar
            if (sound.source.mute)
            {
                sound.source.mute = false;
            }
            else // dan menghilangkan bisu ke semua sound yang di bisukan(mute)
            {
                sound.source.mute = true;
            }
        }
    }

    // mengganti semua volume sound yang ada di array
    public void changeVolume(float volume)
    {
        // untuk setiap sound yang ada di array sounds
        foreach (var sound in sounds)
        {
            // volumenya di ubah sesuai dengan parameter yang ditetapkan
            sound.source.volume = volume;
        }
    }
}
