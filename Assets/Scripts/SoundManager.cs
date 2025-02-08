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

[Serializable]
public class SoundPlayedData
{
    public float lastPlayedTime;
    public float currentPitch = 1.0f;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] List<SoundData> sounds = new List<SoundData>();

    Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();

    [Header("Pitch Increase Settings")]
    [SerializeField] private float pitchIncreaseAmount;
    [SerializeField] private float maxPitch;
    [SerializeField] private float resetTime;

    Dictionary<string, SoundPlayedData> soundPlayHistory = new Dictionary<string, SoundPlayedData>();

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
            audioSource.pitch = 1;
            audioSource.PlayOneShot(soundData.audioClip, soundData.volume);
        }
    }

    public void PlaySound(string soundName, float volume)
    {
        if (soundDictionary.TryGetValue(soundName, out SoundData soundData))
        {
            audioSource.pitch = 1;
            audioSource.PlayOneShot(soundData.audioClip, volume);
        }
    }

    public void PlaySoundWithPitchIncrease(string soundName)
    {
        if (soundDictionary.TryGetValue(soundName, out SoundData soundData))
        {
            float currentTime = Time.time;

            if (!soundPlayHistory.ContainsKey(soundName))
            {
                soundPlayHistory[soundName] = new SoundPlayedData();
            }

            SoundPlayedData playData = soundPlayHistory[soundName];

            if (currentTime - playData.lastPlayedTime > resetTime)
            {
                playData.currentPitch = 1.0f;
            }
            else
            {
                playData.currentPitch = Mathf.Clamp(playData.currentPitch + pitchIncreaseAmount, 1.0f, maxPitch);
            }

            audioSource.pitch = playData.currentPitch;
            audioSource.PlayOneShot(soundData.audioClip, soundData.volume);
            playData.lastPlayedTime = currentTime;
        }
    }
}

