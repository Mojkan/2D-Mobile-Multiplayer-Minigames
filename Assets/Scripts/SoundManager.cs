using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoundData
{
    public string soundName;
    public float volume;
    public AudioClip audioClip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] List<SoundData> sounds = new List<SoundData>();

    Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (var sound in sounds)
        {
            soundDictionary.Add(sound.soundName, sound);
        }
    }

    public void PlaySound(string soundName)
    {
        if (soundDictionary.TryGetValue(soundName, out SoundData soundData))
        {
            audioSource.PlayOneShot(soundData.audioClip, soundData.volume);
        }
    }

    public void PlaySound(string soundName, float volume)
    {
        if (soundDictionary.TryGetValue(soundName, out SoundData soundData))
        {
            audioSource.PlayOneShot(soundData.audioClip, volume);
        }
    }
}

