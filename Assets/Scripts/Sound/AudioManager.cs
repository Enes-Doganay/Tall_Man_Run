using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Serializable]
    class SoundIDClipPair
    {
        public SoundID m_SoundID;
        public AudioClip m_AudioClip;
    }
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource effectSource;
    [SerializeField, Min(0f)]
    private float minSoundInterval = 0.1f;
    [SerializeField]
    private SoundIDClipPair[] sounds;

    private float lastSoundPlayTime;
    readonly Dictionary<SoundID, AudioClip> clips = new();

    AudioSettings audioSettings = new();

    public bool EnableMusic
    {
        get => audioSettings.EnableMusic;
        set
        {
            audioSettings.EnableMusic = value;
            musicSource.mute = !value;
        }
    }

    // Unmute/mute all sound effects
    public bool EnableSfx
    {
        get => audioSettings.EnableSfx;
        set
        {
            audioSettings.EnableSfx = value;
            effectSource.mute = !value;
        }
    }

    // The Master volume of the audio listener
    public float MasterVolume
    {
        get => audioSettings.MasterVolume;
        set
        {
            audioSettings.MasterVolume = value;
            AudioListener.volume = value;
        }
    }

    void Start()
    {
        foreach (var sound in sounds)
        {
            clips.Add(sound.m_SoundID, sound.m_AudioClip);
        }
    }

    void PlayMusic(AudioClip audioClip, bool looping = true)
    {
        if (musicSource.isPlaying)
            return;

        musicSource.clip = audioClip;
        musicSource.loop = looping;
        musicSource.Play();
    }

    // Play a music based on its sound ID
    public void PlayMusic(SoundID soundID, bool looping = true)
    {
        PlayMusic(clips[soundID], looping);
    }

    // Stop the current music
    public void StopMusic()
    {
        musicSource.Stop();
    }

    void PlayEffect(AudioClip audioClip)
    {
        if (Time.time - lastSoundPlayTime >= minSoundInterval) //Þuanki zamandan son ses çalma zamanýný çýkarttýðýmýzda minimum ses aralýðýndan büyükse
        {
            effectSource.PlayOneShot(audioClip); //Effect sesini bir kez çal
            lastSoundPlayTime = Time.time; //Son ses çalma zamanýna þuanki zamaný ata
        }
    }

    // Play a sound effect based on its sound ID
    public void PlayEffect(SoundID soundID)
    {
        if (soundID == SoundID.None)
            return;

        PlayEffect(clips[soundID]);
    }
}