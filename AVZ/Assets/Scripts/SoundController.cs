using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource thisAudioSource;
    [SerializeField] VolumeType audioType;

    private void Awake()
    {
       if (thisAudioSource == default)
        {
            thisAudioSource = GetComponent<AudioSource>();
        }

        SoundManager.onVolumeChanged += OnVolumeChanged;
    }

    public void OnVolumeChanged(float volume, List<VolumeType> affected)
    {
        if (!affected.Contains(audioType))
        {
            return;
        }

        thisAudioSource.volume = volume;
    }

    private void OnDestroy()
    {
        SoundManager.onVolumeChanged -= OnVolumeChanged;
    }
}
