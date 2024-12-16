using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public float musicVolume;
    public float sfxVolume;

    [SerializeField] private string startMusic;

    private void Start()
    {
        PlayMusic(startMusic);
    }

    public void setMusicSource(AudioSource source)
    {
        musicSource = source;
        musicSource.volume = musicVolume;
    }
    public void setSFXSource(AudioSource source)
    {
        sfxSource = source;
        sfxSource.volume = sfxVolume;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.soundName == name);
        if (sound == null) { Debug.Log("Sound " + name + " not found"); }
        else{ musicSource.clip = sound.clip; musicSource.Play(); }
    }
    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.soundName == name);
        if (sound == null) { Debug.Log("sfx " + name + " not found"); }
        else{ sfxSource.clip = sound.clip; sfxSource.Play(); }
    }
}
