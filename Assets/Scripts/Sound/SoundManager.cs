using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    #region SINGLETON
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get { return instance; }
    }

    #endregion

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public AudioClip[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    [SerializeField] private AudioClip startingMusic;
    [SerializeField] private AudioClip buttonClickMusic;

    private void Start()
    {
        PlayMusic(startingMusic);
    }

    public void SetMusicVolume(float volume) 
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }

    public float GetSFXVolume()
    {
        return sfxSource.volume;
    }

    public void PlayMusic(AudioClip audio) 
    {
        AudioClip sound = Array.Find(musicSounds, x => x == audio);
        musicSource.clip = sound;
        musicSource.Play();
    }

    public void PlayClickButton() 
    {
        AudioClip sound = Array.Find(sfxSounds, x => x == buttonClickMusic);
        sfxSource.clip = sound;
        sfxSource.Play();
    }
}
