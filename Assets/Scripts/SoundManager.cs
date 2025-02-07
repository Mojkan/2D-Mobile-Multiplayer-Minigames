using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoundData
{
    public string soundName;
    public AudioClip audioClip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] List<SoundData> audioClips = new List<SoundData>();

    Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();

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

        foreach (var sound in audioClips)
        {
            soundDictionary.Add(sound.soundName, sound.audioClip);
        }
    }

    public void PlaySound(string soundName, float volume)
    {
        if (soundDictionary.TryGetValue(soundName, out AudioClip audioClip))
        {
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioClip);
        }
    }
}

